using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class csvReader : MonoBehaviour
{

    TextAsset csvFile;
    List<string[]> csvDatas = new List<string[]>();

    // Start is called before the first frame update
    void Start()
    {
        csvFile = Resources.Load("raintest") as TextAsset;
        StringReader reader = new StringReader(csvFile.text);

        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            csvDatas.Add(line.Split(','));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
