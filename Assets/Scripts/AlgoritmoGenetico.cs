using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System;
using UnityEngine;

public class AlgoritmoGenetico : MonoBehaviour
{
    //fitness function
    public double accuracy(int[] solution, string candidate)
    {
        int n_gene_matches = 0;


        for (int i = 0; i < solution.Length; i++)
        {
            if (solution[i] == (int)Char.GetNumericValue(candidate[i]))
                n_gene_matches += 1;
        }


        return n_gene_matches / solution.Length;
    }


    DataTable randomGeneration(int generation_size,int genes)
    {
        DataTable generationTable=new DataTable("generation");
        DataColumn column = new DataColumn();

        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "Secuence";
        generationTable.Columns.Add(column);
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Chromosome";
        generationTable.Columns.Add(column);
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "Generation";
        generationTable.Columns.Add(column);
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.String");
        column.ColumnName = "Birth";
        generationTable.Columns.Add(column);
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Double");
        column.ColumnName = "Fitness";
        generationTable.Columns.Add(column);
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Int32");
        column.ColumnName = "Parents";
        generationTable.Columns.Add(column);
        column = new DataColumn();
        column.DataType = System.Type.GetType("System.Boolean");
        column.ColumnName = "Elite";
        generationTable.Columns.Add(column);



        List<int[]> generation = new List<int[]>();
        int i = 0;
        while(i<generation_size)
        {
            DataRow row = generationTable.NewRow();
            row["Secuence"] = i + 1;
            string ch = "";
            System.Random rnd = new System.Random();
            for (int j = 0; j < genes; j++)
            {
                ch = ch + rnd.Next(0, 2);
            }
            row["Chromosome"] = ch;
            row["Generation"] = 1;
            row["Birth"] = "Random";
            row["Parents"] = 0;
            row["Elite"] = false;
            generationTable.Rows.Add(row);
            i++;
        }

        Debug.Log("RandomGen");

        return generationTable;
    }

    DataTable assignElites(DataTable gen,float eliteRate)
    {
        int generationSize = gen.Rows.Count;
        int elites = ((int)(generationSize * eliteRate));
        int tamano=gen.Select().Length;
        for(int i =0;i<tamano;i++)
            gen.Rows[i]["Elite"] = false;
        DataView dt = gen.DefaultView;
        dt.Sort = "Fitness desc";
        DataTable Sorted = dt.ToTable();
        for(int i =0;i<elites;i++)
        {
            Sorted.Rows[i]["Elite"] = true;
        }
        return Sorted;
    }
        
   DataRow[] selectElites(DataTable gen)
    {
        DataRow[] Elites = gen.Select();
        List<DataRow> LElite = new List<DataRow>();
        foreach(DataRow row in Elites)
        {
            if(bool.Parse(row["Elite"].ToString()) == true)
                LElite.Add(row);
        }
        DataRow[] RowArray = LElite.ToArray();
        //int poolSize = Convert.ToInt32(gen.Compute("max([Secuence])", string.Empty));

        int poolSize = int.MinValue;
        foreach (DataRow dr in gen.Rows)
        {
            int numero = (int)dr["Secuence"];
            poolSize = Math.Max(poolSize, numero);
        }
        Debug.Log("MAX pool: " + poolSize);

        int genMax = int.MinValue;
        foreach (DataRow dr in gen.Rows)
        {
            int numero = (int)dr["Generation"];
            genMax = Math.Max(genMax, numero);
        }

        Debug.Log("MAX gen: " + genMax);

        int i = 0;
        foreach (DataRow Elite in RowArray)
        {
            Elite["Parents"] = Elite["Secuence"];
            Elite["Secuence"] = poolSize + i + 1;
            Elite["Birth"] = "Elitism";
            Elite["Elite"] = false;
            Elite["Generation"] = genMax + 1;
            i++;
        }

        return Elites;
    }

    DataTable createMutants(DataTable gen, int mutants, float bitFlipRate)
    {
        int lastSeq = int.MinValue;
        foreach (DataRow dr in gen.Rows)
        {
            int numero = (int)dr["Secuence"];
            lastSeq = Math.Max(lastSeq, numero);
        }

        int lastGen = int.MinValue;
        foreach (DataRow dr in gen.Rows)
        {
            int numero = (int)dr["Generation"];
            lastGen = Math.Max(lastGen, numero);
        }

        DataRow[] EliteParents= gen.Select("Birth = 'Elitism'");
        int n_elites = EliteParents.Length;
        int i = 0;
        while(i<mutants)
        {
            DataRow mutant = gen.NewRow();
            mutant["Secuence"] = lastSeq + i + 1;
            mutant["Generation"] = lastGen;
            mutant["Birth"] = "Mutation";
            mutant["Elite"] = false;

            int parentIndex=UnityEngine.Random.Range(0, n_elites);
            DataRow parent = EliteParents[parentIndex];
            //?? Cambiar Secuence a String en el caso de requerirlo;
            mutant["Parents"] = int.Parse(parent["Secuence"].ToString());
            string bitsToFlip="";
            string parString=parent["Chromosome"].ToString();
            for (int j = 0; j < parString.Length; j++)
            {
                //UnityEngine.Random.Range(1,100);//Suponiendo rate porcentual
                float randi=UnityEngine.Random.Range(0f, 1f);//Suponiendo rate unitario
                if (randi > bitFlipRate)
                    bitsToFlip += "1";
                else
                    bitsToFlip += "0";
            }
            string cromosoma = "";
            for(int j=0;j<parString.Length;j++)
            {
                cromosoma += parString[j] ^ bitsToFlip[j];
            }

            mutant["Chromosome"] = cromosoma;
            try
            {
                gen.Rows.Add(mutant);
            } catch(Exception ex)
            {

            };
            i++;
        }
        return gen;
    }

    DataTable createSplices(DataTable gen, int nSplicePairs)
    {
        int lastSeq = int.MinValue;
        foreach (DataRow dr in gen.Rows)
        {
            int numero = (int)dr["Secuence"];
            lastSeq = Math.Max(lastSeq, numero);
        }

        int lastGen = int.MinValue;
        foreach (DataRow dr in gen.Rows)
        {
            int numero = (int)dr["Generation"];
            lastGen = Math.Max(lastGen, numero);
        }

        DataRow[] EliteParents = gen.Select("Birth = 'Elitism'");
        int n_elites = EliteParents.Length;

        int i = 0;
        while(i<nSplicePairs)
        {
            DataRow splice = gen.NewRow();
            splice["Generation"] = lastGen;
            splice["Birth"] = "Splice Pair";
            splice["Elite"] = false;
            int parent1_index = UnityEngine.Random.Range(0, n_elites);
            DataRow parent1 = EliteParents[parent1_index];
            DataTable dt = gen.Clone();
            dt.Rows.Add(EliteParents);
            dt.Rows.RemoveAt(parent1_index);

            DataRow[] elPr = dt.Select();
            DataRow parent2 = elPr[UnityEngine.Random.Range(0, n_elites - 1)];
            int rando = UnityEngine.Random.Range(1, parent1["Chromosome"].ToString().Length);
            string[] splices=new string[2];
            string split1par1= parent1["Chromosome"].ToString().Substring(0, rando-1); 
            string split2par1= parent1["Chromosome"].ToString().Substring(rando);
            string split1par2= parent2["Chromosome"].ToString().Substring(0, rando-1);
            string split2par2= parent2["Chromosome"].ToString().Substring(rando);

            splices[0] = split1par1 + split2par2;
            splices[1] = split1par2 + split2par1;

            splice["Chromosome"] = splices[0];
            splice["Secuence"] = lastSeq + i + 1;

            gen.Rows.Add(splice);

            splice["Chromosome"] = splices[1];
            splice["Secuence"] = lastSeq + i + 2;

            gen.Rows.Add(splice);
            i++;
        }

        return gen;
    }


    DataTable fill_Random(DataTable gen, int generation_size, int genes)
    {
        int lastSeq = int.MinValue;
        foreach (DataRow dr in gen.Rows)
        {
            int numero = (int)dr["Secuence"];
            lastSeq = Math.Max(lastSeq, numero);
        }
        int lastGen = int.MinValue;
        foreach (DataRow dr in gen.Rows)
        {
            int numero = (int)dr["Generation"];
            lastGen = Math.Max(lastGen, numero);
        }

        System.Random rand = new System.Random();
        int i = gen.Rows.Count;
        while(i<generation_size)
        {
            DataRow row = gen.NewRow();
            row["Secuence"] = lastSeq + i + 1;
            string ch = "";
            for (int j = 0; j < genes; j++)
            {
                ch = ch + rand.Next(0, 2);
            }
            row["Chromosome"] = ch;
            row["Generation"] = lastGen;
            row["Birth"] = "random";
            row["Parents"] = 0;
            row["Elite"] = false;
            
            try
            {
                gen.Rows.Add(row);
            }catch(Exception ex)
            {
            };
            i++;
        }
        return gen;
    }



    DataTable create_descendents(DataTable genePool, float eliteRate, int[] solution, double stopLimit)
    {
        DataTable nextGen = genePool;
        int genSize = nextGen.Rows.Count;

        while(Convert.ToDouble(genePool.Compute("max([Fitness])", string.Empty)) < stopLimit)
        {
            DataRow[] dr = selectElites(nextGen);
            Debug.Log("SELECTION");
            nextGen = genePool.Clone();
            foreach (DataRow row in dr)
            {
                nextGen.ImportRow(row);
            }
            float splicePairRate = eliteRate / 2;
            int nSplicePairs = (int)splicePairRate * genSize;
            nextGen = createSplices(nextGen, nSplicePairs);
            Debug.Log("SPLICES");

            float mutantRate = 0.60f;
            float bitFlipRate = 0.01f;
            int nMutants = (int)mutantRate * genSize;

            nextGen = createMutants(nextGen, nMutants, bitFlipRate);
            Debug.Log("MUTANTES");
            nextGen = fill_Random(nextGen, genSize, 100);
            Debug.Log("RANDOM");
            //Traducir esto y fitness de ship
            foreach (DataRow row in nextGen.Rows)
            {
                row["Fitness"] = accuracy(solution, row["Chromosome"].ToString());
            }
            Debug.Log("FITNESS");
            float neliteRate = 0.20f;
            nextGen = assignElites(nextGen, neliteRate);
            Debug.Log("Elites Asignados");
            DataRow[] dt = nextGen.Select();
            nextGen = new DataTable();
            foreach(DataRow row in dt)
            {
                genePool.ImportRow(row);
            }
            Debug.Log("Next loop prepared");
            //genePool.Rows.Add(nextGen.Select());

            //Debug.Log("GeneracionActual: " + nextGen.Rows[0]["Generation"].ToString());

        }

        return genePool;
    }

    public int[] solve(int[] solution, int generationSize, double stopLimit)
    {
        Debug.Log("Llego al genetico");
        DataTable genePool = randomGeneration(generationSize, 100);
        foreach(DataRow row in genePool.Rows)
        {
            row["Fitness"] = accuracy(solution, row["Chromosome"].ToString());
        }
        //genePool["Fitness"] = nextGen.apply(lambda row: ship.accuracy(row.Chromosome, solution), axis - 1);
        float eliteRate = 0.20f;
        genePool = assignElites(genePool, eliteRate);
        Debug.Log("Iniciamos loop");
        genePool = create_descendents(genePool, eliteRate, solution, stopLimit);
        

        DataRow[] candidatos = genePool.Select("Fitness = stopLimit");
        string canditatoSolution = candidatos[0]["Chromosome"].ToString();

        int[] sol = new int[100];
        for (int i = 0; i < canditatoSolution.Length; i++)
        {
            sol[i] = (int)Char.GetNumericValue(canditatoSolution[i]);
            
        }

        return sol;
    }

}


