using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEXP.Utils
{
    class GenerateSimulation
    {
        private string dataPath;
        private List<List<List<string>>> overall;
        private List<List<List<string>>> individual;
        private int numberOfParticipants = 1;
        private int numberOfConditions = 1;
        private System.IO.StreamWriter file;

        public GenerateSimulation()
        {
            dataPath = Directory.GetCurrentDirectory() + "\\";
            overall = new List<List<List<string>>>();
            individual = new List<List<List<string>>>();

        }

        public void ShowSimulation()
        {
            GenerateOverallArrangement();
            GenerateIndivisualArrangement();
            WriteToJson();
        }

        private void WriteToJson()
        {
            file = new System.IO.StreamWriter(dataPath + "test.json", false);
            file.WriteLine("{");
            file.WriteLine(WrapperNameLine("Experiment", 0));
            file.WriteLine("\"children\": [");

            int i;
            for (i = 0; i < individual.Count - 1; i++)
            {
                // Format: seperate as { participant }. Don't include char ',' .
                WriteParticipant(i);
                file.WriteLine(",");
            }
            // Write the last participant without ',' appended.
            WriteParticipant(i);

            file.WriteLine("]");
            file.WriteLine("}");

            file.Close();
        }

        private void WriteParticipant(int idParticipant)
        {
            file.WriteLine("{");
            file.WriteLine(WrapperNameLine("Participant " + idParticipant.ToString(), 0));
            file.WriteLine("\"children\": [");

            int i;
            for (i = 0; i < MainWindow.datas.arrangement.block - 1; i++)
            {
                WriteBlock(idParticipant, i);
                file.WriteLine(",");
            }
            WriteBlock(idParticipant, i);

            file.WriteLine("]");
            file.WriteLine("}");
        }

        private void WriteBlock(int idParticipant, int idBlock)
        {
            file.WriteLine("{");
            file.WriteLine(WrapperNameLine("Block " + idBlock.ToString(), 0));
            file.WriteLine("\"children\": [");

            int i;
            for (i = 0; i < individual[idParticipant].Count - 1; i++)
            {
                WriteCondition(idParticipant, i);
                file.WriteLine(",");
            }
            WriteCondition(idParticipant, i);

            file.WriteLine("]");
            file.WriteLine("}");
        }

        private void WriteCondition(int idParticipant, int idCondition)
        {
            file.WriteLine("{");
            string tmp = "(";
            int i;

            for (i = 0; i < individual[idParticipant][idCondition].Count - 1; i++)
            {
                tmp += individual[idParticipant][idCondition][i] + ", ";
            }
            tmp += individual[idParticipant][idCondition][i] + ")";

            file.WriteLine(WrapperNameLine("Condition " + idCondition.ToString() + ": " + tmp, 0));
            file.WriteLine("\"children\": [");

            Random random = new Random();
            int[] randomSet = Enumerable.Range(1, MainWindow.datas.arrangement.trial).OrderBy(t => random.Next()).ToArray();
            
            int n;
            for (n = 0; n < MainWindow.datas.arrangement.trial - 1; n++)
            {
                file.WriteLine(WrapperNameLine("Trial " + randomSet[n].ToString(), 1));
                file.WriteLine(",");
            }
            file.WriteLine(WrapperNameLine("Trial " + randomSet[n].ToString(), 1));

            file.WriteLine("]");
            file.WriteLine("}");

        }

        // Input: string value
        // Output: "name": "value"
        private string WrapperNameLine(string example, int mode)
        {
            switch (mode)
            {
                case 0:
                    // Append ',': e.g., "name": "cluster",
                    return "\"name\": \"" + example + "\",";
                case 1:
                    // For example, {"name": "AgglomerativeCluster"}
                    return "{\"name\": \"" + example + "\"}";
                default:
                    return example;
            }
        }

        private void GenerateIndivisualArrangement()
        {
            foreach (var idv in overall) 
            {
                numberOfParticipants *= idv.Count;
            }

            // After getting index for each participant, generate conditions.
            // The number of conditions may not be the same as the number of participants.
            foreach (var idv in MainWindow.datas.independentVariables)
                numberOfConditions *= idv.levels.Count;

            // 'i' is the id of participant.
            for (int i = 0; i < numberOfParticipants; i++)
            {
                List<List<string>> participant = new List<List<string>>();

                int total = numberOfParticipants;
                int tmp = i;

                // For each participant, generate index in each idv.
                // For example, for participant 0 and there are 3 idvs [3, 2, 2]. Then, the index of participant 0 is (0, 0, 0).
                // The index of participant 5 is (1, 0, 1).
                int[] index = new int[overall.Count];
                for (int j = 0; j < overall.Count; j++)
                {
                    total /= overall[j].Count;
                    index[j] = tmp / total;
                    tmp -= index[j] * total;
                }

                Log.getLogInstance().writeLog(numberOfConditions.ToString());

                for (int k = 0; k < numberOfConditions; k++)
                {
                    List<string> condition = new List<string>();

                    int tmpK = k;
                    int totalConditions = numberOfConditions;

                    for (int m = 0; m < overall.Count; m++)
                    {
                        totalConditions /= MainWindow.datas.independentVariables[m].levels.Count;
                        int n = tmpK / totalConditions;
                        condition.Add(overall[m][index[m]][n]);
                        tmpK -= n * totalConditions;
                    }

                    participant.Add(condition);
                }


                individual.Add(participant);
            }

        }

        private void GenerateOverallArrangement()
        {
            //List<List<string>> techniqueArrangement = new List<List<string>>();
            // Assume default strategy for technique is Latin-square.
            //List<string> techniques = new List<string>();
            //techniques.Add(MainWindow.datas.researchQuestion.hypothesis.mainSolution);
            
            foreach (var idv in MainWindow.datas.independentVariables)
            {
                List<List<string>> idvArrangement = new List<List<string>>();

                // Collect name of levels of idv.
                List<string> levelsName = new List<string>();
                foreach (var level in idv.levels)
                {
                    levelsName.Add(level.name);
                }

                switch (idv.counterBalance)
                {
                    case COUNTERBALANCE.FullyCounterBalancing:

                        // Generate permutation.
                        foreach (var i in Permutate(levelsName, levelsName.Count))
                        {
                            List<string> permutationSample = new List<string>();
                            foreach (string j in i)
                            {
                                permutationSample.Add(j);
                            }
                            idvArrangement.Add(permutationSample);
                        }

                        break;

                    case COUNTERBALANCE.LatinSquare:

                        for (int i = 0; i < levelsName.Count; i++)
                        {
                            List<string> latinSquareSample = new List<string>();
                            int j = i;
                            for (int count = 0; count < levelsName.Count; count++)
                            {
                                latinSquareSample.Add(levelsName[(j+levelsName.Count)%levelsName.Count]);
                                j++;
                            }
                            idvArrangement.Add(latinSquareSample);
                        }

                            break;

                    case COUNTERBALANCE.NoCounterBalancing:

                        idvArrangement.Add(levelsName);

                        break;
                }

                overall.Add(idvArrangement);
            }
        }

        private void RotateRight(IList sequence, int count)
        {
            object tmp = sequence[count - 1];
            sequence.RemoveAt(count - 1);
            sequence.Insert(0, tmp);
        }

        private IEnumerable<IList> Permutate(IList sequence, int count)
        {
            // http://www.codeproject.com/Articles/43767/A-C-List-Permutation-Iterator

            if (count == 1) yield return sequence;
            else
            {
                for (int i = 0; i < count; i++)
                {
                    foreach (var perm in Permutate(sequence, count - 1))
                        yield return perm;
                    RotateRight(sequence, count);
                }
            }
        }
    }
}
