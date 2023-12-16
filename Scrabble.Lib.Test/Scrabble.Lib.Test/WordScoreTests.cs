using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Scrabble.Lib.Test
{
    [TestFixture]
    public partial class WordScoreTests
    {
        public abstract class WordsTest
        {
            public Game ScrabbleGame { get; set; }
            protected IList<string> PlayerNames;
            protected IList<TilePoint[]> Words;
            protected IList<int> ExpectedScores;
            protected string TilesToSelect;
            protected FakeTileBag TileBag;

            [SetUp]
            protected void CreateGame()
            {
                TileBag = FakeTileBag.Create(TilesToSelect);
                ScrabbleGame = Game.Create(GameTests.GetPlayers(), Board.Create(), TileBag);
            }

            protected void LayTheWords()
            {
                for (var i = 0; i < Words.Count(); ++i)
                {
                    ScrabbleGame.LayWord(i % 2 == 0 ? "Victoria" : "Albert", Words[i]);
                    var result = ScrabbleGame.AcceptWord();
                    Assert.That(result.ValidWord, Is.True);
                    Assert.That(result.PlayerScore, Is.EqualTo(ExpectedScores[i]));
                }
            }
        }

        //TODO: Start here

        #region Initial word must go through the centre square, and scores double the tile values (centre square is effectively a Double Word Score)
        public class LayingFirstWord : WordsTest
        {
            /*
             Word =
             | |H| |
             | |E| |
             | |N| |
             */

            public LayingFirstWord()
            {
                TilesToSelect = "HENABCD";
            }

            [SetUp]
            public void CreateWordsAndScores()
            {
                PlayerNames = new[] { "Victoria" };

                Words = new[] {
                    new[] {
                        TilePoint.Create('H', "H8"),
                        TilePoint.Create('E', "I8"),
                        TilePoint.Create('N', "J8")
                    }
                };

                ExpectedScores = new[] { 12 };
            }

            [Test]
            public void ThenScoresExtendedWord()
            {
                LayTheWords();
            }
        }
        #endregion

        #region Initial word is extended horizontally with suffix
        /*Scores value of all letter tiles (no new bonus squares used)*/
        //public class ExtendingAWordHorizontallySuffix : WordsTest
        //{
        //    /*
        //     Word 1 (H on centre) =
        //     |H|E| | |
        //     | | | | |
        //     | | | | |
        //     * 
        //     Word 2 =
        //     |H|E|A|D|
        //     | | | | |
        //     | | | | |
        //     */

        //    public ExtendingAWordHorizontallySuffix()
        //    {
        //        TilesToSelect = "HEABCDE" + "ADFGHIJ" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        PlayerNames = new[] { "Victoria" };

        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H8"),
        //                TilePoint.Create('E', "H9")
        //            },
        //            new[] {
        //                TilePoint.Create('A', "H10"),
        //                TilePoint.Create('D', "H11"),
        //            }
        //        };

        //        ExpectedScores = new[] { 10, 8 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is extended horizontally with prefix      
        //Scores value of all letter tiles (no new bonus squares used)
        //public class ExtendingAWordHorizontallyPrefix : WordsTest
        //{
        //    /*
        //     Word 1 (H on centre) =
        //     | |H|E| |
        //     | | | | |
        //     | | | | |
        //     * 
        //     Word 2 =
        //     |T|H|E| |
        //     | | | | |
        //     | | | | |
        //     */

        //    public ExtendingAWordHorizontallyPrefix()
        //    {
        //        TilesToSelect = "HEABCDE" + "TFGHIJK" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        PlayerNames = new[] { "Victoria" };

        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H8"),
        //                TilePoint.Create('E', "H9")
        //            },
        //            new[] {
        //                TilePoint.Create('T', "H7"),
        //            }
        //        };

        //        ExpectedScores = new[] { 10, 6 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is extended vertically with suffix
        //Scores value of all letter tiles (no new bonus squares used)
        //public class ExtendingAWordVerticallySuffix : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | | |H|
        //     | | |E|
        //     | | | |
        //     * 
        //     Word 2 =
        //     | | |H|
        //     | | |E|
        //     | | |A|
        //     | | |D|
        //     */

        //    public ExtendingAWordVerticallySuffix()
        //    {
        //        TilesToSelect = "HEABCDE" + "ADFGHIJ" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        PlayerNames = new[] { "Victoria" };

        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H8"),
        //                TilePoint.Create('E', "I8")
        //            },
        //            new[] {
        //                TilePoint.Create('A', "J8"),
        //                TilePoint.Create('D', "K8")
        //            }
        //        };

        //        ExpectedScores = new[] { 10, 8 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is extended vertically with prefix
        //Scores value of all letter tiles (no new bonus squares used)
        //public class ExtendingAWordVerticallyPrefix : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | | | |
        //     | | |H|
        //     | | |E|
        //     * 
        //     Word 2 =
        //     | | |T|
        //     | | |H|
        //     | | |E|
        //     */

        //    public ExtendingAWordVerticallyPrefix()
        //    {
        //        TilesToSelect = "HEABCDE" + "TFGHIJK" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        PlayerNames = new[] { "Victoria" };

        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H8"),
        //                TilePoint.Create('E', "I8")
        //            },
        //            new[] {
        //                TilePoint.Create('T', "G8")
        //            }
        //        };

        //        ExpectedScores = new[] { 10, 6 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is extended by a horizontal word below        
        //Scores value of all letter tiles for both words
        //public class ExtendingAWordUnderneath : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | |B| |
        //     | |O| |
        //     | |T| |
        //     | | | |
        //     * 
        //     Word 2 =
        //     | |B| |
        //     | |O| |
        //     | |T| |
        //     |S|H|Y|
        //     */

        //    public ExtendingAWordUnderneath()
        //    {
        //        TilesToSelect = "BOTABCD" + "SHYEFGH" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        PlayerNames = new[] { "Victoria" };

        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('B', "H8"),
        //                TilePoint.Create('O', "I8"),
        //                TilePoint.Create('T', "J8")
        //            },
        //            new[] {
        //                TilePoint.Create('S', "K7"),
        //                TilePoint.Create('H', "K8"),
        //                TilePoint.Create('Y', "K9")
        //            }
        //        };

        //        ExpectedScores = new[] { 10, 18 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is extended by a horizontal word below        
        //Scores new word(+ bonuses) + (tile + bonus) for the letter extending the word + (tile scores without bonus) for existing tiles in extended word.
        //public class ExtendingAWordUnderneathWithLetterBonus : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | | |H|
        //     | | |E|
        //     | | | |
        //     * 
        //     Word 2 =
        //     | | |H|
        //     | | |E|
        //     |M|A|N|
        //     */

        //    public ExtendingAWordUnderneathWithLetterBonus()
        //    {
        //        TilesToSelect = "HEABCDE" + "MANFGHI" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        PlayerNames = new[] { "Victoria" };

        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H8"),
        //                TilePoint.Create('E', "I8")
        //            },
        //            new[] {
        //                TilePoint.Create('M', "J6"), //TL
        //                TilePoint.Create('A', "J7"),
        //                TilePoint.Create('N', "J8")
        //            }
        //        };

        //        ExpectedScores = new[] { 10, 17 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is extended by a horizontal word above
        //public class ExtendingAWordAbove : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | | | |
        //     | | |A|
        //     | | |T|
        //     * 
        //     Word 2 =
        //     |T|I|C|
        //     | | |A|
        //     | | |T|
        //     */

        //    public ExtendingAWordAbove()
        //    {
        //        TilesToSelect = "ATABCDE" + "TICFGHI" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('A', "H8"),
        //                TilePoint.Create('T', "I8")
        //            },
        //            new[] {
        //                TilePoint.Create('T', "G6"),
        //                TilePoint.Create('I', "G7"), //DL
        //                TilePoint.Create('C', "G8")
        //            }
        //        };

        //        ExpectedScores = new[] { 4, 11 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is extended by a vertical word to the left
        //public class ExtendingAWordLeft : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | |A|T|
        //     | | | |
        //     | | | |
        //     * 
        //     Word 2 =
        //     |R|A|T|
        //     |U| | |
        //     |B| | |
        //     */

        //    public ExtendingAWordLeft()
        //    {
        //        TilesToSelect = "ATABCDE" + "RUBFGHI" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('A', "H8"),
        //                TilePoint.Create('T', "H9")
        //            },
        //            new[] {
        //                TilePoint.Create('R', "H7"),
        //                TilePoint.Create('U', "I7"), //DL
        //                TilePoint.Create('B', "J7")
        //            }
        //        };

        //        ExpectedScores = new[] { 4, 9 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is extended by a vertical word to the right
        //public class ExtendingAWordRight : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     |O|R| |
        //     | | | |
        //     | | | |
        //     * 
        //     Word 2 =
        //     |O|R|B|
        //     | | |E|
        //     | | |G|
        //     */

        //    public ExtendingAWordRight()
        //    {
        //        TilesToSelect = "ORABCDE" + "BEGFGHI" + "ABCDE";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[]{
        //            new[] {
        //                TilePoint.Create('O', "H8"),
        //                TilePoint.Create('R', "H9")
        //            },
        //            new[] {
        //                TilePoint.Create('B', "H10"),
        //                TilePoint.Create('E', "I10"),
        //                TilePoint.Create('G', "J10") //TL
        //            }
        //        };

        //        ExpectedScores = new[] { 4, 15 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is intersected. Just scores points for the tiles laid (+ bonuses)
        //public class CrossingAWordVertically : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | | | |
        //     |H|E|N|
        //     | | | |
        //     * 
        //     Word 2 =
        //     | |M| |
        //     |H|E|N|
        //     | |A| |
        //     | |D| |
        //     */

        //    public CrossingAWordVertically()
        //    {
        //        TilesToSelect = "HENABCD" + "MADEFGH" + "ABCDEF";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H7"),
        //                TilePoint.Create('E', "H8"),
        //                TilePoint.Create('N', "H9")
        //            },
        //            new[] {
        //                TilePoint.Create('M', "G8"),
        //                TilePoint.Create('A', "I8"),
        //                TilePoint.Create('D', "J8")
        //            }
        //        };

        //        ExpectedScores = new[] { 12, 7 };
        //    }

        //    [Test]
        //    public void ThenScoreIncludesCrossedTile()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Initial word is intersected. Just scores points for the tiles laid (+ bonuses)
        //public class CrossingAWordHorizontally : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | |H| |
        //     | |E| |
        //     | |N| |
        //     * 
        //     Word 2 =
        //     | |H| | |
        //     |M|E|A|D|
        //     | |N| | |
        //     */

        //    public CrossingAWordHorizontally()
        //    {
        //        TilesToSelect = "HENABCD" + "MADEFGH" + "ABCDEF";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "G8"),
        //                TilePoint.Create('E', "H8"),
        //                TilePoint.Create('N', "I8")
        //            },
        //            new[] {
        //                TilePoint.Create('M', "H7"),
        //                TilePoint.Create('A', "H9"),
        //                TilePoint.Create('D', "H10")
        //            }
        //        };

        //        ExpectedScores = new[] { 12, 7 };
        //    }

        //    [Test]
        //    public void ThenScoreIncludesCrossedTile()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region New word intersects an existing word, and creates two additional new words
//        public class ExtendingMultipleWords : WordsTest
//        {
//            /*
//             Word 1 =
//              5 6 7 8
//g            | | | | |
//h            | | | |A|
//i            | | | |T|
//             * 
//             Word 2 =
//              5 6 7 8
//g            | |T|I|C|
//h            | | | |A|
//i            | | | |T|
//             * 
//             Word 3 =
//              5 6 7 8
//g            | |T|I|C|
//h            | | | |A|
//i            | |B|U|T|
//             * 
//             Word 4 =
//              5 6 7 8
//g            | |T|I|C|
//h            | | | |A|
//i            | |B|U|T|
//j            | | | |S|
//             * 
//             Word 5 =
//              5 6 7 8
//g            | |T|I|C|
//h            | | | |A|
//i            | |B|U|T|
//j            |M|E|S|S|
//             */

//            public ExtendingMultipleWords()
//            {
//                TilesToSelect = "ATBUMES" + "TICSGHI" + "ABCDEFGHIJK";
//            }

//            [SetUp]
//            public void CreateWordsAndScores()
//            {
//                Words = new[] {
//                    new[] {
//                        TilePoint.Create('A', "H8"),
//                        TilePoint.Create('T', "I8")
//                    },
//                    new[] {
//                        TilePoint.Create('T', "G6"),
//                        TilePoint.Create('I', "G7"), //DL
//                        TilePoint.Create('C', "G8")
//                    },
//                    new[] {
//                        TilePoint.Create('B', "I6"),
//                        TilePoint.Create('U', "I7") //DL
//                    },
//                    new[] {
//                        TilePoint.Create('S', "J8")
//                    },
//                    new[] {
//                        TilePoint.Create('M', "J5"),
//                        TilePoint.Create('E', "J6"),
//                        TilePoint.Create('S', "J7")
//                    }
//                };

//                ExpectedScores = new[] { 4, 11, 10, 17, 26 };
//            }

//            [Test]
//            public void ThenScoresExtendedWords()
//            {
//                LayTheWords();
//            }
//        }
        #endregion

        #region Laying all tiles in one go scores a nonus 50 points
        //public class UsingAllTiles : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | | | |
        //     |H|E|N|
        //     | | | |
        //     * 
        //     Word 2 =
        //     | |B| |
        //     |H|E|N|
        //     | |D| |
        //     | |P| |
        //     | |O| |
        //     | |S| |
        //     | |T| |
        //     | |S| |
        //     */

        //    public UsingAllTiles()
        //    {
        //        TilesToSelect = "HENABCD" + "BDPOSTS" + "ABCDEFGHIJ";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H7"),
        //                TilePoint.Create('E', "H8"),
        //                TilePoint.Create('N', "H9")
        //            },
        //            new[] {
        //                TilePoint.Create('B', "G8"),
        //                TilePoint.Create('D', "I8"),
        //                TilePoint.Create('P', "J8"),
        //                TilePoint.Create('O', "K8"),
        //                TilePoint.Create('S', "L8"),
        //                TilePoint.Create('T', "M8"),
        //                TilePoint.Create('S', "N8")
        //            }
        //        };

        //        ExpectedScores = new[] { 12, 64 };
        //    }

        //    [Test]
        //    public void ThenScoresBingoBonus()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region New word extends initial word, spans two bonus squares and uses all tiles
        //public class SpanTwoWordBonusSquares : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | | | |B| | | |
        //     | | | |E| | | |
        //     | | | |E| | | |
        //     | | | | | | | |
        //     * 
        //     Word 2 =
        //     | | | |B| | | |
        //     | | | |E| | | |
        //     | | | |E| | | |
        //     |A|M|O|N|G|S|T|
        //     */

        //    public SpanTwoWordBonusSquares()
        //    {
        //        TilesToSelect = "BEEABCD" + "AMONGST" + "ABCDEFGHIJ";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('B', "H8"),
        //                TilePoint.Create('E', "I8"),
        //                TilePoint.Create('E', "J8")
        //            },
        //            new[] {
        //                TilePoint.Create('A', "K5"),//DW
        //                TilePoint.Create('M', "K6"),
        //                TilePoint.Create('O', "K7"),
        //                TilePoint.Create('N', "K8"),
        //                TilePoint.Create('G', "K9"),
        //                TilePoint.Create('S', "K10"),
        //                TilePoint.Create('T', "K11")//DW
        //            }
        //        };

        //        ExpectedScores = new[] { 10, 96 /*uses 7 tiles*/ };
        //    }

        //    [Test]
        //    public void ThenScoresAllBonuses()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Single tile creates a horizontal word between two existing tiles
        //public class BridgingTwoLettersHorizontally : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | | | |
        //     |H|E|N|
        //     | | | |
        //     * 
        //     Word 2 =
        //     |S| | |
        //     |H|E|N|
        //     |I| | |
        //     |P| | |
        //     * 
        //     Word 3 =
        //     |S| |O|
        //     |H|E|N|
        //     |I| |L|
        //     |P| |Y|
        //     * 
        //     Word 4 =
        //     |S| |O|
        //     |H|E|N|
        //     |I| |L|
        //     |P|A|Y|
        //     */

        //    public BridgingTwoLettersHorizontally()
        //    {
        //        TilesToSelect = "HENOLYA" + "SIPABCD" + "ABCDEFGHIJ";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H7"),
        //                TilePoint.Create('E', "H8"),
        //                TilePoint.Create('N', "H9")
        //            },
        //            new[] {
        //                TilePoint.Create('S', "G7"),
        //                TilePoint.Create('I', "I7"),
        //                TilePoint.Create('P', "J7")
        //            },
        //            new[] {
        //                TilePoint.Create('O', "G9"),
        //                TilePoint.Create('L', "I9"),
        //                TilePoint.Create('Y', "J9")
        //            },
        //            new[] {
        //                TilePoint.Create('A', "J8")
        //            }
        //        };

        //        ExpectedScores = new[] { 12, 11, 21, 19 };
        //    }

        //    [Test]
        //    public void ThenScoreIncludesBothExistingTiles()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Single tile creates a vertical word between two existing tiles
        //public class BridgingTwoLettersVertically : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     | |H| |
        //     | |E| |
        //     | |N| |
        //     * 
        //     Word 2 =
        //     |S|H|I|P|
        //     | |E| | |
        //     | |N| | |
        //     * 
        //     Word 3 =
        //     |S|H|I|P|
        //     | |E| | |
        //     |O|N|L|Y|
        //     * 
        //     Word 4 =
        //     |S|H|I|P|
        //     | |E| |A|
        //     |O|N|L|Y|
        //     */

        //    public BridgingTwoLettersVertically()
        //    {
        //        TilesToSelect = "HENOLYA" + "SIPABCD" + "ABCDEFGHIJ";
        //    }

        //    [SetUp]
        //    public void CreateWordsAndScores()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "G8"),
        //                TilePoint.Create('E', "H8"),
        //                TilePoint.Create('N', "I8")
        //            },
        //            new[] {
        //                TilePoint.Create('S', "G7"),
        //                TilePoint.Create('I', "G9"),
        //                TilePoint.Create('P', "G10")
        //            },
        //            new[] {
        //                TilePoint.Create('O', "I7"),
        //                TilePoint.Create('L', "I9"),
        //                TilePoint.Create('Y', "I10")
        //            },
        //            new[] {
        //                TilePoint.Create('A', "H10")
        //            }
        //        };

        //        ExpectedScores = new[] { 12, 11, 21, 19 };
        //    }

        //    [Test]
        //    public void ThenScoreIncludesBothExistingTiles()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Single tile creates horizontal and vertical words between existing tiles
        //public class FillingASquareWithSingleTile : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     |H| | |
        //     |E| | |
        //     |N| | |
        //     * 
        //     Word 2 =
        //     |H|A|T|
        //     |E| | |
        //     |N| | |
        //     * 
        //     Word 3 =
        //     |H|A|T|
        //     |E| |O|
        //     |N| |W|
        //     * 
        //     Word 4 =
        //     |H|A|T|
        //     |E| |O|
        //     |N|O|W|
        //     * 
        //     Word 5 =
        //     |H|A|T|
        //     |E|G|O|
        //     |N|O|W|
        //     */

        //    public FillingASquareWithSingleTile()
        //    {
        //        TilesToSelect = "HENOWGA" + "ATOABCD" + "ABCDEFGHI";
        //    }

        //    [SetUp]
        //    public void ThenWordsInBothDirectionsAreScored()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "G8"),
        //                TilePoint.Create('E', "H8"),
        //                TilePoint.Create('N', "I8")
        //            },
        //            new[] {
        //                TilePoint.Create('A', "G9"),
        //                TilePoint.Create('T', "G10")
        //            },
        //            new[] {
        //                TilePoint.Create('O', "H10"),
        //                TilePoint.Create('W', "I10")
        //            },
        //            new[] {
        //                TilePoint.Create('O', "I9")
        //            },
        //            new[] {
        //                TilePoint.Create('G', "H9")
        //            }
        //        };

        //        ExpectedScores = new[] { 12, 7, 18, 14, 26 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Multiple tiles create a new horizontal word and two vertical words between existing tiles
        //public class FillingASquareWithMultipleTilesHorizontally : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     |H| | |
        //     |E| | |
        //     |N| | |
        //     * 
        //     Word 2 =
        //     |H|A|T|
        //     |E| | |
        //     |N| | |
        //     * 
        //     Word 3 =
        //     |H|A|T|
        //     |E| | |
        //     |N|O|W|
        //     * 
        //     Word 4 =
        //     |H|A|T|
        //     |E|G|O|
        //     |N|O|W|
        //     */

        //    public FillingASquareWithMultipleTilesHorizontally()
        //    {
        //        TilesToSelect = "HENOWAB" + "ATGOABC" + "ABCDEFGHI";
        //    }

        //    [SetUp]
        //    public void ThenAllExtendedWordsAreScored()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "G8"),
        //                TilePoint.Create('E', "H8"),
        //                TilePoint.Create('N', "I8")
        //            },
        //            new[] {
        //                TilePoint.Create('A', "G9"),
        //                TilePoint.Create('T', "G10")
        //            },
        //            new[] {
        //                TilePoint.Create('O', "I9"),
        //                TilePoint.Create('W', "I10")
        //            },
        //            new[] {
        //                TilePoint.Create('G', "H9"),
        //                TilePoint.Create('O', "H10")
        //            }
        //        };

        //        ExpectedScores = new[] { 12, 7, 19, 21 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion

        #region Multiple tiles create a new vertical word and two horizontal words between existing tiles
        //public class FillingASquareWithMultipleTilesVertically : WordsTest
        //{
        //    /*
        //     Word 1 =
        //     |H|E|N|
        //     | | | |
        //     | | | |
        //     * 
        //     Word 2 =
        //     |H|E|N|
        //     |A| | |
        //     |T| | |
        //     * 
        //     Word 3 =
        //     |H|E|N|
        //     |A| |O|
        //     |T| |W|
        //     * 
        //     Word 4 =
        //     |H|E|N|
        //     |A|G|O|
        //     |T|O|W|
        //     */

        //    public FillingASquareWithMultipleTilesVertically()
        //    {
        //        TilesToSelect = "HENOWAB" + "ATGOABC" + "ABCDEFGHI";
        //    }

        //    [SetUp]
        //    public void ThenAllExtendedWordsAreScored()
        //    {
        //        Words = new[] {
        //            new[] {
        //                TilePoint.Create('H', "H7"),
        //                TilePoint.Create('E', "H8"),
        //                TilePoint.Create('N', "H9")
        //            },
        //            new[] {
        //                TilePoint.Create('A', "I7"),
        //                TilePoint.Create('T', "J7")
        //            },
        //            new[] {
        //                TilePoint.Create('O', "I9"),
        //                TilePoint.Create('W', "J9")
        //            },
        //            new[] {
        //                TilePoint.Create('G', "I8"),
        //                TilePoint.Create('O', "J8")
        //            }
        //        };

        //        ExpectedScores = new[] { 12, 7, 19, 21 };
        //    }

        //    [Test]
        //    public void ThenScoresExtendedWord()
        //    {
        //        LayTheWords();
        //    }
        //}
        #endregion
    }
}