using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Onirim
{

    class GameCard
    {

        public Rectangle drawingRect{get;set;}

        //public const int cardWidth = 137;
        //public const int cardHeight = 237;

        public const int cardWidth = 113;
        public const int cardHeight = 168;
        public const int numberOfCardTypes=17;

        private int textureOffset = 0;

        public const int backgroundOffset= cardWidth*numberOfCardTypes;

        private Rectangle textureRect;

        private CardTypeEnum cardType;
        private CardTypeClassEnum cardTypeClass;
        private CardTypeColorEnum cardColor;

        public Rectangle BackgroundRect
        {
            get {
                Rectangle result = new Rectangle(backgroundOffset,0,cardWidth,cardHeight);
                return result;
            }
        }

        public CardTypeEnum CardType
        {
            get { return cardType; }
        }

        public CardTypeClassEnum CardTypeClass
        {
            get { return cardTypeClass; }
        }

        public CardTypeColorEnum CardColor
        {
            get { return cardColor; }
        }

        public GameCard(CardTypeEnum type)
        {
            cardType = type;
            switch (cardType)
            {
                case CardTypeEnum.BLUEDOOR:
                    this.cardTypeClass = CardTypeClassEnum.DOOR;
                    break;
                case CardTypeEnum.BLUEKEY:
                    this.cardTypeClass = CardTypeClassEnum.KEY;
                    textureOffset = cardWidth;
                    break;
                case CardTypeEnum.BLUESUN:
                    this.cardTypeClass = CardTypeClassEnum.SUN;
                    textureOffset = cardWidth * 2;
                    break;
                case CardTypeEnum.BLUEMOON:
                    this.cardTypeClass = CardTypeClassEnum.MOON;
                    textureOffset = cardWidth * 3;
                    break;
                case CardTypeEnum.GREENDOOR:
                    this.cardTypeClass = CardTypeClassEnum.DOOR;
                    textureOffset = cardWidth * 4;
                    break;
                case CardTypeEnum.GREENKEY:
                    this.cardTypeClass = CardTypeClassEnum.KEY;
                    textureOffset = cardWidth * 5;
                    break;
                case CardTypeEnum.GREENSUN:
                    this.cardTypeClass = CardTypeClassEnum.SUN;
                    textureOffset = cardWidth * 6;
                    break;
                case CardTypeEnum.GREENMOON:
                    this.cardTypeClass = CardTypeClassEnum.MOON;
                    textureOffset = cardWidth * 7;
                    break;
                case CardTypeEnum.REDDOOR:
                    this.cardTypeClass = CardTypeClassEnum.DOOR;
                    textureOffset = cardWidth * 8;
                    break;
                case CardTypeEnum.REDKEY:
                    this.cardTypeClass = CardTypeClassEnum.KEY;
                    textureOffset = cardWidth * 9;
                    break;
                case CardTypeEnum.REDSUN:
                    this.cardTypeClass = CardTypeClassEnum.SUN;
                    textureOffset = cardWidth * 10;
                    break;
                case CardTypeEnum.REDMOON:
                    this.cardTypeClass = CardTypeClassEnum.MOON;
                    textureOffset = cardWidth * 11;
                    break;
                case CardTypeEnum.ORANGEDOOR:
                    this.cardTypeClass = CardTypeClassEnum.DOOR;
                    textureOffset = cardWidth * 12;
                    break;
                case CardTypeEnum.ORANGEKEY:
                    this.cardTypeClass = CardTypeClassEnum.KEY;
                    textureOffset = cardWidth * 13;
                    break;
                case CardTypeEnum.ORANGESUN:
                    this.cardTypeClass = CardTypeClassEnum.SUN;
                    textureOffset = cardWidth * 14;
                    break;
                case CardTypeEnum.ORANGEMOON:
                    this.cardTypeClass = CardTypeClassEnum.MOON;
                    textureOffset = cardWidth * 15;
                    break;
                case CardTypeEnum.NIGHTMARE:
                    textureOffset = cardWidth * 16;
                    break;
            }
            textureRect = new Rectangle(textureOffset,0,cardWidth,cardHeight);

        }

        public Boolean isKey()
        {
            return this.cardTypeClass == CardTypeClassEnum.KEY;
        }

        public Boolean isDoor()
        {
            return this.cardTypeClass == CardTypeClassEnum.DOOR;
        }

        public Boolean isSun()
        {
            return this.cardTypeClass == CardTypeClassEnum.SUN;
        }

        public Boolean isMoon()
        {
            return this.cardTypeClass == CardTypeClassEnum.MOON;
        }

        public Boolean isNightmare()
        {
            return this.cardTypeClass == CardTypeClassEnum.NIGHTMARE;
        }

        public Rectangle TextureRect {
            get { return this.textureRect;}
        }
    }
}