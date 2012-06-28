using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Onirim
{
    class Hand
    {
        private List<GameCard> cardsOnHand;

        const int handLimit = 5;

        const float percentualDistanceHeight = 0.1f;

        private Rectangle handsRectangle=new Rectangle();

        public Hand()
        {
            this.cardsOnHand = new List<GameCard>();
            this.cardsRatio = GameCard.cardWidth / GameCard.cardHeight;
        }

        public List<GameCard> CardsOnHand
        {
            get { return this.cardsOnHand; }
        }

        public void updateHandsWidth(int screenWidth,int screenHeight)
        {
            this.handsRectangle.X = (int) Math.Round(screenWidth/2.0-cardsOnHand.Count/2.0*GameCard.cardWidth*CardProperties.cardShrink);
            this.handsRectangle.Y = (int)Math.Round(screenHeight * (1 - percentualDistanceHeight) - GameCard.cardHeight * CardProperties.cardShrink);
            this.handsRectangle.Width = (int)Math.Round(cardsOnHand.Count * GameCard.cardWidth * CardProperties.cardShrink);
            this.handsRectangle.Height = (int)Math.Round(GameCard.cardHeight * CardProperties.cardShrink);

            //Now update cards positions

            for (int i = 0; i < this.cardsOnHand.Count;i++ )
            {
                GameCard card = this.cardsOnHand[i];
                card.drawingRect = new Rectangle((int)Math.Round(this.handsRectangle.X + i * GameCard.cardWidth * CardProperties.cardShrink), this.handsRectangle.Y, (int)Math.Round(GameCard.cardWidth * CardProperties.cardShrink), (int)Math.Round(GameCard.cardHeight * CardProperties.cardShrink));
            }
        }

        public void addCardToHand(GameCard card){
            if (this.cardsOnHand.Count>=handLimit)
            throw new Exception("Hand-Limit reached");
            this.cardsOnHand.Add(card);
        }

        public int cardsRatio { get; set; }
    }
}
