using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ShootyGame
{
    public struct PlayerScore
    {
        public int scorevalue;
        public string name;
    }

    class HighscoreFile
    {

        XmlDocument HighscoreDocument;
        List<PlayerScore> m_playerScores;
        
        public HighscoreFile() 
        {
            HighscoreDocument = new XmlDocument();
            m_playerScores = new List<PlayerScore>();
            
            CreateDefaultDocument();
            WritetoFile(45, "Wtf?");
            WritetoFile(90, "Wohoo!");
            ReadFile();
        }


        public void CreateDefaultDocument()
        {

            if (!File.Exists("Highscores.xml"))
            {
                XmlNode RootNode = HighscoreDocument.CreateElement("Highscores");
                HighscoreDocument.AppendChild(RootNode);

                //Score is the element, Value is the points, Inner text is the name
                XmlNode ScoreNode = HighscoreDocument.CreateElement("Score");
                XmlAttribute ScoreValue = HighscoreDocument.CreateAttribute("Value");
                ScoreValue.Value = "0";
                ScoreNode.Attributes.Append(ScoreValue);
                ScoreNode.InnerText = "UserName";
                RootNode.AppendChild(ScoreNode);

                HighscoreDocument.Save("Highscores.xml");
            }
        }



        /// <summary>
        /// Reads in the highscore file 
        /// If the object was created correctly there should always be an
        /// available XML file since its created on construction
        /// </summary>
        public void ReadFile()
        {
            HighscoreDocument.Load("Highscores.xml");
            XmlNodeList Scorenodes = HighscoreDocument.SelectNodes("//Highscores/Score");
            m_playerScores.Clear();

            foreach(XmlNode scorenode in Scorenodes)
            {
                PlayerScore Playerscoreref;
                Playerscoreref.scorevalue = Int32.Parse(scorenode.Attributes["Value"].Value);
                Playerscoreref.name = scorenode.InnerText;
                m_playerScores.Add(Playerscoreref);
            }
        }

        /// <summary>
        /// Opens up an existing file and writes a score to it
        /// An example of how you would use this function is as such
        /// WritetoFile(45, "Insertname?");
        /// </summary>
        /// <param name="score"></param>
        /// <param name="name"></param>
        public void WritetoFile(int score, string name) 
        {
            HighscoreDocument.Load("Highscores.xml");

            XmlNode root = HighscoreDocument.DocumentElement;

            //Score is the element, Value is the points, Inner text is the name
            XmlNode ScoreNode = HighscoreDocument.CreateElement("Score");
            XmlAttribute ScoreValue = HighscoreDocument.CreateAttribute("Value");
            ScoreValue.Value = score.ToString();
            ScoreNode.Attributes.Append(ScoreValue);
            ScoreNode.InnerText = name;
            root.AppendChild(ScoreNode);

            HighscoreDocument.Save("Highscores.xml");


           
        }

   
        
        


    }
}
