using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Onirim
{
    class Stock
    {

        private List<GameCard> cards;

        public const float percentualDistanceHeight = 0.1f;
        public const float percentualDistanceWidth = 0.1f;

        private int oldWidth=0;

        private int oldHeight=0;

        private Rectangle drawingRect;

        public Rectangle DrawingRect
        {
            get { return drawingRect;}
        }

        public Stock()
        {
            this.cards = new List<GameCard>();
        }

        public void initializeCards(CardProperties props)
        {
            for (int i = 1; i <= props.Count; i++)
            {
                GameCard newCard = new GameCard(props.Type);
                this.cards.Add(newCard);
            }
        }

        public Boolean hasCards()
        {
            return this.cards.Count >= 1;
        }

        public void shuffleStock()
        {
            this.cards = ShuffleList<GameCard>(this.cards);
        }

        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }

        public GameCard popCardFromStock()
        {
            int lastIndex = cards.Count - 1;
            GameCard card = cards[lastIndex];
            cards.RemoveAt(lastIndex);
            return card;
        }

        public GameCard pullCardFromStock()
        {
            int lastIndex = cards.Count - 1;
            GameCard card = cards[lastIndex];
            return card;
        }

        public void updateStockposition(int screenWidth,int screenHeight)
        {
            if ((screenWidth != oldWidth) || (screenHeight != oldHeight))
            {
                drawingRect = new Rectangle((int)Math.Round(screenWidth*(1-percentualDistanceWidth) - GameCard.cardWidth * CardProperties.cardShrink),
                    (int)Math.Round(screenHeight * percentualDistanceHeight),
                    (int)Math.Round(GameCard.cardWidth * CardProperties.cardShrink),
                    (int)Math.Round(GameCard.cardHeight * CardProperties.cardShrink));
                
                //Set the drawingRect
                foreach (GameCard card in cards)
                {
                    card.drawingRect=new Rectangle(drawingRect.X,drawingRect.Y,drawingRect.Width,drawingRect.Height);
                }

                oldWidth = screenWidth;
                oldHeight = screenHeight;
            }
        }



        public void returnCardsToStock(List<GameCard> list)
        {
            foreach (GameCard card in list)
            {
                //update grapicalCardPosition
                card.drawingRect = new Rectangle(drawingRect.X, drawingRect.Y, drawingRect.Width, drawingRect.Height);
                //putt cardOnStock
                cards.Add(card);
            }
            //clear list
            list.Clear();
            //shuffle Stock
            this.shuffleStock();
        }
    }
}
