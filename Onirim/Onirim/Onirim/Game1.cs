using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;

namespace Onirim
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Stock stock;
        Hand hand;
        Texture2D cardTexture;
        List<GameCard> limbus;
        List<GameCard> trash;
        int selectedCard;
        Boolean handSelected;

        enum GameState {STARTMENU, REGULAR_PLAYING, WAITING_FOR_NIGHTMARE_HANDLING, WAITING_FOR_DOOR_HANDLING};
        GameState currentState = GameState.REGULAR_PLAYING;

        StorageDevice device;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            limbus = new List<GameCard>();
            trash = new List<GameCard>();
            selectedCard = 0;
            handSelected = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            device = this.initializeStorageDevice();
            CardProperties[] gameCards = Content.Load<CardProperties[]>(@"GameProperties\gameProperties");
            stock = new Stock();
            foreach (CardProperties prop in gameCards){
                stock.initializeCards(prop);
            }
            stock.shuffleStock();
            //gameProperties.Cards.Add(new CardProperties(CardTypeEnum.GREENDOOR, 10));
            //Console.Write(gameCards.Length);
            //saveProperties("test.xml",gameProperties);
            cardTexture = Content.Load<Texture2D>(@"Textures\Cards2");
            hand = new Hand();
            

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //this.Exit();
            //Fill hand up to 5 cards or exit if not possible
            Boolean exit=fillHand(false);
            if (exit) {
                Console.Write("Player looses");
                this.Exit();
            }


            stock.updateStockposition(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            hand.updateHandsWidth(this.Window.ClientBounds.Width, this.Window.ClientBounds.Height);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            switch(currentState)
            {
                case GameState.REGULAR_PLAYING:
                    this.drawHand(selectedCard,handSelected,null);
                    this.drawStock(false);
                    break;
                case GameState.STARTMENU:

                    break;
                case GameState.WAITING_FOR_NIGHTMARE_HANDLING:

                    break;
                case GameState.WAITING_FOR_DOOR_HANDLING:
                    //Show the taken door on Stock

                    break;
            
            }
            base.Draw(gameTime);
        }

        private void drawHand(int indexOfSelectedCard, Boolean userSelectedHand,List<GameCard> highlightedCards)
        {
            spriteBatch.Begin();
            int currentIndex = 0;
            foreach (GameCard card in hand.CardsOnHand){
                if (userSelectedHand && indexOfSelectedCard == currentIndex)
                {
                    //draw selected Card
                    Rectangle cardDrawingRect = new Rectangle();
                    cardDrawingRect.X = card.drawingRect.X;
                    cardDrawingRect.Y = card.drawingRect.Y-(int) Math.Round(GameCard.cardHeight*CardProperties.cardShrink/5);
                    cardDrawingRect.Width = card.drawingRect.Width;
                    cardDrawingRect.Height = card.drawingRect.Height;
                    spriteBatch.Draw(cardTexture, cardDrawingRect, card.TextureRect, Color.White);
                }
                else
                {
                    spriteBatch.Draw(cardTexture, card.drawingRect, card.TextureRect, Color.White);
                }
                currentIndex++;
            }
            spriteBatch.End();
        }

        private void drawStock(Boolean showCardOnStock)
        {
            //Draw the backside of cards and the number of cards in Stock
            if (showCardOnStock)
            {
                //Draw the card ontop the stock
                spriteBatch.Begin();
                GameCard card = stock.pullCardFromStock();
                spriteBatch.Draw(cardTexture, card.drawingRect, card.TextureRect, Color.White);
                spriteBatch.End();
            }
            else
            {
                spriteBatch.Begin();
                GameCard card = stock.pullCardFromStock();
                spriteBatch.Draw(cardTexture, card.drawingRect, card.BackgroundRect, Color.White);
                spriteBatch.End();
            }
        }

        private Boolean fillHand(Boolean regularTurn)
        {
            Console.Write("Fill Hand");
            while(hand.CardsOnHand.Count<5)
            {
                if (!stock.hasCards())
                {
                    //The game is over
                    return true;
                }
                GameCard newCard=stock.pullCardFromStock();
                if (newCard.isNightmare())
                {
                    Console.Write("Nightmare on stock");
                    if (regularTurn){
                        Console.Write("Nightmare must be handled");
                        currentState = GameState.WAITING_FOR_NIGHTMARE_HANDLING;
                        return false;
                    }else{
                        Console.Write("Nightmare put to limbus");
                        limbus.Add(stock.popCardFromStock());
                    }
                }
                else
                {
                    if (newCard.isDoor())
                    {
                        Console.Write("Door on stock");
                        if (regularTurn)
                        {
                            Console.Write("Door must be handled");
                            currentState = GameState.WAITING_FOR_DOOR_HANDLING;
                            return false;
                        }
                        else
                        {
                            Console.Write("Door put to limbus");
                            //the card has to be thrown to the limbus
                            limbus.Add(stock.popCardFromStock());
                        }
                    }
                    else
                    {
                        Console.Write("Regular card on stock");
                        hand.addCardToHand(stock.popCardFromStock());
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// This is called when a door is taken within a regular turn:
        /// If the player has a key in the corresponding color he is able to play this key to obtain the card or put the card on the limbus.
        /// if the player chooses to play a Door te index of the propper key wihtin the hand is returned
        /// </summary>
        /// <param name="door">The door, which has to be handled</param>
        private int handleDoor(GameCard door)
        {
            int index = -1;
            for (int i = 0; i < hand.CardsOnHand.Count; i++)
            {
                if ((hand.CardsOnHand[i].isKey()) && (door.CardColor == hand.CardsOnHand[i].CardColor))
                {
                    index = i;
                    break;
                }
            }
            if (index != -1)
            {
                //Propper key found. Show Dialog to ask user whether to use Key for door aquisition or not

                //By default say yes
            }
            return index;
        }

        

        /// <summary>
        /// This is called when a nightmare is taken within a regular turn:
        /// The player now has the right to choose between several effects:
        /// 
        /// </summary>
        private void handleNightmare()
        {
            
        }

        private StorageDevice initializeStorageDevice()
        {
            IAsyncResult result = StorageDevice.BeginShowSelector(PlayerIndex.One, null, null);
            while (!result.IsCompleted)
            {
                // Call Update and Draw to draw the guide
                this.Tick();
            }
            StorageDevice device = StorageDevice.EndShowSelector(result);
            return device;
        }

        
    }
}
