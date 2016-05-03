using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void analysis_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "";
            label6.Text = "";
            label7.Text = "";
            

            int posMatched = 0, negMatched = 0,x,z,c;
            

            //  user string value

            string userString = input.Text;
          showSentiment.Text = "Review =  " +userString;
            string[] words = userString.Split(' ');
            int userSentenceLength = words.Length;


            //file path positive Emoticons

            string filePathPositiveEmoticons = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\positive-emoticons-words.txt";
            string positiveEmoticonstext = System.IO.File.ReadAllText(filePathPositiveEmoticons);
            string[] posEmoticonsFileWords = positiveEmoticonstext.Split(new char[] { ',', '\r', '\n' });

            int posEmoticonsFileLength = posEmoticonsFileWords.Length;

            //file path Negative Emoticons
            string filePathNegativeEmoticons = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negative-emoticon-words.txt";
            string negativeEmoticonstext = System.IO.File.ReadAllText(filePathNegativeEmoticons);
            string[] negativeEmoticonsWord = negativeEmoticonstext.Split(new char[] { ',', '\r', '\n' });
            int negEmoticonsFileLength = negativeEmoticonsWord.Length;


            //file path positive

            string filePathPositive = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\positive-words.txt";
            string positivetext = System.IO.File.ReadAllText(filePathPositive);
            string[] posFileWords = positivetext.Split(new char[] { ',', '\r', '\n' });
          
            int posFileLength = posFileWords.Length;

            //Negative file path
            string filePathNegative = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negative-words.txt";
            string negativetext = System.IO.File.ReadAllText(filePathNegative);
            string[] negativeWord = negativetext.Split(new char[] { ',', '\r', '\n' });
            int negFileLength = negativeWord.Length;

            //Negation file pth

            string filePathNegation = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negation-words.txt";
            string negationtext = System.IO.File.ReadAllText(filePathNegation);
            string[] negationWord = negationtext.Split(new char[] { ',', '\r', '\n' });
            int negationFileLength = negationWord.Length;







            // Handling Negation

            int negword1Matched=0 , poswordMatched=0;


            // conversion pos To neg

           

                for (x = 0; x < userSentenceLength - 1; x++)
                {
                    for (z = 0; z < posFileLength; z++)

                    {
                        for (c = 0; c < negationWord.Length; c++)
                        {
                            if (words[x] == negationWord[c])

                            {
                                if (words[x + 1] == posFileWords[z])

                                {
                                    negword1Matched++;


                                }

                            }

                        }

                    }
                }

                // conversion neg to positive

                for (x = 0; x < userSentenceLength - 1; x++)
                {
                    for (z = 0; z < negFileLength; z++)

                    {
                        for (c = 0; c < negationWord.Length; c++)
                        {
                            if (words[x] == negationWord[c])

                            {
                                if (words[x + 1] == negativeWord[z])

                                {
                                    poswordMatched++;


                                }

                            }

                        }

                    }
                }






                //Loop for Positive



                for (x = 0; x < userSentenceLength; x++)
                {
                    for (z = 0; z < posFileLength; z++)
                    {
                        if (words[x] == posFileWords[z])
                        {

                            posMatched++;
                        }
                    }


                }

                // Loop for Negative Words

                for (x = 0; x < userSentenceLength; x++)
                {
                    for (z = 0; z < negFileLength; z++)
                    {
                        if (words[x] == negativeWord[z])
                        {
                            negMatched++;

                        }
                    }


                }

                int posEmoticonsMatched = 0, negEmoticonsMatched = 0;

                // Loop for Positive Emoticons

                for (x = 0; x < userSentenceLength; x++)
                {
                    for (z = 0; z < posEmoticonsFileLength; z++)
                    {
                        if (words[x] == posEmoticonsFileWords[z])
                        {
                            posEmoticonsMatched++;

                        }
                    }


                }


                // Loop for Negative Emoticons

                for (x = 0; x < userSentenceLength; x++)
                {
                    for (z = 0; z < negEmoticonsFileLength; z++)
                    {
                        if (words[x] == negativeEmoticonsWord[z])
                        {
                            negEmoticonsMatched++;

                        }
                    }


                }



                double totPos = posMatched + poswordMatched - negword1Matched + posEmoticonsMatched;
                double totNeg = negMatched + negword1Matched - poswordMatched + negEmoticonsMatched;

                //Probabilty

                double sumPosNeg = totPos + totNeg;
                double negProbability = (totNeg / sumPosNeg);
                double posProbabilty = (totPos / sumPosNeg);


                label1.Text = "Positive Word Probabilty =  " + posProbabilty.ToString();
                label2.Text = "Negative Word Probabilty =  " + negProbability.ToString();

                if (posProbabilty < 0.5)
                {
                    label9.Text = "Polarity = Negative";
                    string imageName = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\images\polarity\negative.jpg";
                    var image = Image.FromFile(imageName);

                    pictureBox1.Image = image;

                }
                else if (posProbabilty > 0.5)
                {
                    label9.Text = "Polarity = Positive";

                    string imageName = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\images\polarity\positive.png";
                    var image = Image.FromFile(imageName);

                    pictureBox1.Image = image;

                }

                else
                {
                    label9.Text = "Polaririty = Neutral";

                    string imageName = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\images\polarity\neutral.png";
                    var image = Image.FromFile(imageName);

                    pictureBox1.Image = image;

                }




            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string imageName = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\images\polarity\bkgrnd.png";
            var image = Image.FromFile(imageName);

            pictureBox1.Image = image;

            label9.Text = "";
            label7.Text = "";
            int posMatched = 0, negMatched = 0, x, z, c, negMatched1 = 0;


            //  user string value

            string userString = input.Text;
             showSentiment.Text ="Review  =  " +userString;
            string[] words = userString.Split(' ');
            int userSentenceLength = words.Length;


            //file path positive Emoticons

            string filePathPositiveEmoticons = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\positive-emoticons-words.txt";
            string positiveEmoticonstext = System.IO.File.ReadAllText(filePathPositiveEmoticons);
            string[] posEmoticonsFileWords = positiveEmoticonstext.Split(new char[] { ',', '\r', '\n' });

            int posEmoticonsFileLength = posEmoticonsFileWords.Length;

            //file path Negative Emoticons
            string filePathNegativeEmoticons = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negative-emoticon-words.txt";
            string negativeEmoticonstext = System.IO.File.ReadAllText(filePathNegativeEmoticons);
            string[] negativeEmoticonsWord = negativeEmoticonstext.Split(new char[] { ',', '\r', '\n' });
            int negEmoticonsFileLength = negativeEmoticonsWord.Length;


            //file path positive

            string filePathPositive = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\positive-words.txt";
            string positivetext = System.IO.File.ReadAllText(filePathPositive);
            string[] posFileWords = positivetext.Split(new char[] { ',', '\r', '\n' });

            int posFileLength = posFileWords.Length;

            //Negtive file path

            string filePathNegative = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negative-words.txt";
            string negativetext = System.IO.File.ReadAllText(filePathNegative);
            string[] negativeWord = negativetext.Split(new char[] { ',', '\r', '\n' });
            int negFileLength = negativeWord.Length;

            //Negation File Path

            string filePathNegation = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negation-words.txt";
            string negationtext = System.IO.File.ReadAllText(filePathNegation);
            string[] negationWord = negationtext.Split(new char[] { ',', '\r', '\n' });
            int negationFileLength = negationWord.Length;

            // Handling Negation

            int negword1Matched = 0, poswordMatched = 0 , PosEmoticonMatched = 0 , negEmoticonMatched = 0;


            // Negation Word Found

            for (x = 0; x < userSentenceLength; x++)
            {
               
                    for (c = 0; c < negationWord.Length; c++)
                    {
                    if (words[x] == negationWord[c])

                    {
                       
                            negword1Matched++;
                            label6.Text = "Negation Word Found  " + negword1Matched.ToString();

                       
                    }

                }
            }


            // Emoticons Found Positive
            for (x = 0; x < userSentenceLength; x++)
            {

                for (c = 0; c < posEmoticonsFileLength; c++)
                {
                    if (words[x] == posEmoticonsFileWords[c])

                    {

                        PosEmoticonMatched++;
                        label3.Text = "Positive Emoticon Found  " + PosEmoticonMatched.ToString();


                    }

                }
            }

            // Emoticons Found Negative
            for (x = 0; x < userSentenceLength; x++)
            {

                for (c = 0; c < negEmoticonsFileLength; c++)
                {
                    if (words[x] == negativeEmoticonsWord[c])

                    {

                        negEmoticonMatched++;
                        label4.Text = "Negative Emoticon Found  " + negEmoticonMatched.ToString();


                    }

                }
            }



            //Loop for Positive



            for (x = 0; x < userSentenceLength; x++)
            {
                for (z = 0; z < posFileLength; z++)
                {
                    if (words[x] == posFileWords[z])
                    {

                        posMatched++;
                    }
                }
                label1.Text ="Positive Words    " + posMatched.ToString();

            }

            // Loop for Negative Words

            for (x = 0; x < userSentenceLength; x++)
            {
                for (z = 0; z < negFileLength; z++)
                {
                    if (words[x] == negativeWord[z])
                    {
                        negMatched++;

                    }
                }

                label2.Text ="Negative Words   " + negMatched.ToString();
            }


          



        }

        private void sentiment_change_button_Click(object sender, EventArgs e)
        {
            string imageName = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\images\polarity\bkgrnd.png";
            var image = Image.FromFile(imageName);

            pictureBox1.Image = image;


            label3.Text = "";
            label4.Text = "";
            label1.Text = "";
            label2.Text = "";
            label9.Text = "";


            int x, z, c;


            //  user string value

            string userString = input.Text;
            showSentiment.Text = "Review  =  " + userString;
            string[] words = userString.Split(' ');
            int userSentenceLength = words.Length;


            //file path positive Emoticons

            string filePathPositiveEmoticons = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\positive-words.txt";
            string positiveEmoticonstext = System.IO.File.ReadAllText(filePathPositiveEmoticons);
            string[] posEmoticonsFileWords = positiveEmoticonstext.Split(new char[] { ',', '\r', '\n' });

            int posEmoticonsFileLength = posEmoticonsFileWords.Length;

            //file path Negative Emoticons
            string filePathNegativeEmoticons = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negative-words.txt";
            string negativeEmoticonstext = System.IO.File.ReadAllText(filePathNegativeEmoticons);
            string[] negativeEmoticonsWord = negativeEmoticonstext.Split(new char[] { ',', '\r', '\n' });
            int negEmoticonsFileLength = negativeEmoticonsWord.Length;


            //file path positive Emoticons

            string filePathPositive = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\positive-words.txt";
            string positivetext = System.IO.File.ReadAllText(filePathPositive);
            string[] posFileWords = positivetext.Split(new char[] { ',', '\r', '\n' });

            int posFileLength = posFileWords.Length;

            //Negtive file path

            string filePathNegative = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negative-words.txt";
            string negativetext = System.IO.File.ReadAllText(filePathNegative);
            string[] negativeWord = negativetext.Split(new char[] { ',', '\r', '\n' });
            int negFileLength = negativeWord.Length;

            //Negation File Path

            string filePathNegation = @"C:\Users\Sharad\Documents\Visual Studio 2015\Projects\WindowsFormsApplication1\WindowsFormsApplication1\dataFile\negation-words.txt";
            string negationtext = System.IO.File.ReadAllText(filePathNegation);
            string[] negationWord = negationtext.Split(new char[] { ',', '\r', '\n' });
            int negationFileLength = negationWord.Length;


            int negword1Matched = 0, poswordMatched = 0;

            // conversion pos To neg

            for (x = 0; x < userSentenceLength - 1; x++)
            {
                for (z = 0; z < posFileLength; z++)

                {
                    for (c = 0; c < negationWord.Length; c++)
                    {
                        if (words[x] == negationWord[c])

                        {
                            if (words[x + 1] == posFileWords[z])

                            {
                                negword1Matched++;
                                label6.Text = "Positive To Negative  =  " + negword1Matched.ToString();

                            }

                        }

                    }

                }
            }

            // conversion neg to positive

            for (x = 0; x < userSentenceLength - 1; x++)
            {
                for (z = 0; z < negFileLength; z++)

                {
                    for (c = 0; c < negationWord.Length; c++)
                    {
                        if (words[x] == negationWord[c])

                        {
                            if (words[x + 1] == negativeWord[z])

                            {
                                poswordMatched++;
                                label7.Text = "Negative To Positive =  " + poswordMatched.ToString();

                            }

                        }

                    }

                }
            }




        }

        private void button2_Click(object sender, EventArgs e)
        {
            /*
            int x, z, c;


            //  user string value

            string userString = input.Text;
            showSentiment.Text = "Review  =  " + userString;
            string[] words = userString.Split(' ');
            int userSentenceLength = words.Length;

            for (x = 0; x < userSentenceLength; x++)
            {

                if (words[x] == "mobile" || words[x] == "Mobile")
                {
                    label12.Text = "Top 10 Mobile Phones 2016";
                    label1.Text = "Galaxy s7";
                    label2.Text = "Galaxy s7 edge";
                    label3.Text = "Lg g5";
                    label4.Text = "Google Nexus 6P";
                    label11.Text = "Apple iPhone 6s";
                    label7.Text = "OnePlus 2";
                    label13.Text = "Vodafone Smart Prime 6";
                    label9.Text = "OnePlus X";
                    label6.Text = "Sony Xperia Z5 ";
                    label10.Text = "Compact BlackBerry Priv";

                }
            }
                   
    */


             
            


        }
    }
    }

