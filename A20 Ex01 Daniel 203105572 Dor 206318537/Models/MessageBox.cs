﻿//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Text;
//using A20_Ex01_Daniel_203105572_Dor_206318537.Utils;
//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using Microsoft.Xna.Framework.Input;
//namespace A20_Ex01_Daniel_203105572_Dor_206318537.Models
//{
//     public class MessageBox
//     {
//          public Game r_Game;

//          public string Text { get; set; }

//          public bool Active { get; private set; }
   
//          public Vector2 Position { get; set; }

//          public Vector2 Size { get; set; }

//          public Color FillColor { get; set; }

       
//          public Color BorderColor { get; set; }
         
//          public Color DialogColor { get; set; }

//          public int BorderWidth { get; set; }

//          private readonly Texture2D _fillTexture;

          
//          private readonly Texture2D _borderTexture;

//          private List<string> _pages;

          
//          private const float DialogBoxMargin = 24f;

//          private Vector2 _characterSize = r_Game.DialogFont.MeasureString(new StringBuilder("W", 1));

//          private int MaxCharsPerLine => (int)Math.Floor((Size.X - DialogBoxMargin) / _characterSize.X);
        
//          private int MaxLines => (int)Math.Floor((Size.Y - DialogBoxMargin) / _characterSize.Y) - 1;
          
//          private int _currentPage;

    
//          private int _interval;

//          private Rectangle TextRectangle => new Rectangle(Position.ToPoint(), Size.ToPoint());

//          private List<Rectangle> BorderRectangles => new List<Rectangle>
//          {
//          // Top (contains top-left & top-right corners)
//          new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y - BorderWidth,
//              TextRectangle.Width + BorderWidth*2, BorderWidth),

//          // Right
//          new Rectangle(TextRectangle.X + TextRectangle.Size.X, TextRectangle.Y, BorderWidth, TextRectangle.Height),

//          // Bottom (contains bottom-left & bottom-right corners)
//          new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y + TextRectangle.Size.Y,
//                TextRectangle.Width + BorderWidth*2, BorderWidth),

//          // Left
//          new Rectangle(TextRectangle.X - BorderWidth, TextRectangle.Y, BorderWidth, TextRectangle.Height)
//          };

//          private Vector2 TextPosition => new Vector2(Position.X + DialogBoxMargin / 2, Position.Y + DialogBoxMargin / 2);

 
//          private Stopwatch _stopwatch;

//          private readonly GameEnvironment r_GameEnvironment = Singelton<GameEnvironment>.Instance;
//          private Vector2 r_CenterScreen;

//          public MessageBox(Game i_Game)
//          {
//               BorderWidth = 2;
//               DialogColor = Color.Black;
//               r_Game = i_Game;

//               FillColor = new Color(1.0f, 1.0f, 1.0f, 0.5f);

//               BorderColor = new Color(0.0f, 0.0f, 0.0f, 1.0f);

//               _fillTexture = new Texture2D(r_Game.GraphicsDevice, 1, 1);
//               _fillTexture.SetData(new[] { FillColor });

//               _borderTexture = new Texture2D(r_Game.GraphicsDevice, 1, 1);
//               _borderTexture.SetData(new[] { BorderColor });

//               _pages = new List<string>();
//               _currentPage = 0;

//               var sizeX = (int)(r_Game.GraphicsDevice.Viewport.Width * 0.5);
//               var sizeY = (int)(r_Game.GraphicsDevice.Viewport.Height * 0.2);

//               Size = new Vector2(sizeX, sizeY);
//               r_CenterScreen = new Vector2(r_GameEnvironment.WindowWidth / 2.0f, r_GameEnvironment.WindowHeight / 2.0f);
//               var posX = r_CenterScreen.X - (Size.X / 2f);
//               var posY = r_GameEnvironment.WindowHeight - Size.Y - 30;

//               Position = new Vector2(posX, posY);
//          }
//          public void Initialize(string text = null)
//          {
//               Text = text ?? Text;

//               _currentPage = 0;

//               Show();
//          }

         
//          public void Show()
//          {
//               Active = true;

//               // use stopwatch to manage blinking indicator
//               _stopwatch = new Stopwatch();

//               _stopwatch.Start();

//               _pages = WordWrap(Text);
//          }

   
//          public void Hide()
//          {
//               Active = false;

//               _stopwatch.Stop();

//               _stopwatch = null;
//          }

         
//          public void Update()
//          {
//               if (Active)
//               {
//                    // Button press will proceed to the next page of the dialog box
//                    if ((r_Game.IsKeyDown(Keys.Enter) && Program.Game.PreviousKeyState.IsKeyUp(Keys.Enter)) ||
//                        (Program.Game.GamePadState.Buttons.A == ButtonState.Pressed))
//                    {
//                         if (_currentPage >= _pages.Count - 1)
//                         {
//                              Hide();
//                         }
//                         else
//                         {
//                              _currentPage++;
//                              _stopwatch.Restart();
//                         }
//                    }

//                    // Shortcut button to skip entire dialog box
//                    if ((Program.Game.KeyState.IsKeyDown(Keys.X) && Program.Game.PreviousKeyState.IsKeyUp(Keys.X)) ||
//                        (Program.Game.GamePadState.Buttons.X == ButtonState.Pressed))
//                    {
//                         Hide();
//                    }
//               }
//          }

        
//          public void Draw(SpriteBatch spriteBatch)
//          {
//               if (Active)
//               {
//                    // Draw each side of the border rectangle
//                    foreach (var side in BorderRectangles)
//                    {
//                         spriteBatch.Draw(_borderTexture, null, side);
//                    }

//                    // Draw background fill texture (in this example, it's 50% transparent white)
//                    spriteBatch.Draw(_fillTexture, null, TextRectangle);

//                    // Draw the current page onto the dialog box
//                    spriteBatch.DrawString(Program.Game.DialogFont, _pages[_currentPage], TextPosition, DialogColor);

//                    // Draw a blinking indicator to guide the player through to the next page
//                    // This stops blinking on the last page
//                    // NOTE: You probably want to use an image here instead of a string
//                    if (BlinkIndicator() || _currentPage == _pages.Count - 1)
//                    {
//                         var indicatorPosition = new Vector2(TextRectangle.X + TextRectangle.Width - (_characterSize.X) - 4,
//                             TextRectangle.Y + TextRectangle.Height - (_characterSize.Y));

//                         spriteBatch.DrawString(Program.Game.DialogFont, ">", indicatorPosition, Color.Red);
//                    }
//               }
//          }


//          private bool BlinkIndicator()
//          {
//               _interval = (int)Math.Floor((double)(_stopwatch.ElapsedMilliseconds % 1000));

//               return _interval < 500;
//          }

//          private List<string> WordWrap(string text)
//          {
//               var pages = new List<string>();

//               var capacity = MaxCharsPerLine * MaxLines > text.Length ? text.Length : MaxCharsPerLine * MaxLines;

//               var result = new StringBuilder(capacity);
//               var resultLines = 0;

//               var currentWord = new StringBuilder();
//               var currentLine = new StringBuilder();

//               for (var i = 0; i < text.Length; i++)
//               {
//                    var currentChar = text[i];
//                    var isNewLine = text[i] == '\n';
//                    var isLastChar = i == text.Length - 1;

//                    currentWord.Append(currentChar);

//                    if (char.IsWhiteSpace(currentChar) || isLastChar)
//                    {
//                         var potentialLength = currentLine.Length + currentWord.Length;

//                         if (potentialLength > MaxCharsPerLine)
//                         {
//                              result.AppendLine(currentLine.ToString());

//                              currentLine.Clear();

//                              resultLines++;
//                         }

//                         currentLine.Append(currentWord);

//                         currentWord.Clear();

//                         if (isLastChar || isNewLine)
//                         {
//                              result.AppendLine(currentLine.ToString());
//                         }

//                         if (resultLines > MaxLines || isLastChar || isNewLine)
//                         {
//                              pages.Add(result.ToString());

//                              result.Clear();

//                              resultLines = 0;

//                              if (isNewLine)
//                              {
//                                   currentLine.Clear();
//                              }
//                         }
//                    }
//               }

//               return pages;
//          }
//     }
//}
