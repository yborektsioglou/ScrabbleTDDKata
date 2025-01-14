﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Scrabble.Lib
{
    public class BoardScoreCalculator
    {
        public static int ScoreWord(IEnumerable<(Square Square, Tile Tile)> laidTiles, IEnumerable<Square> boardSquares)
        {
            
            bool isLayingHorizontally = laidTiles.Any(t => t.Square.Point.X != laidTiles.First().Square.Point.X);
            bool isLayingVertically = laidTiles.Any(t => t.Square.Point.Y != laidTiles.First().Square.Point.Y);
            int score = 0;
            int wordFactor = 1;

            var extendedScore = 0;
            extendedScore += CalculateExtendedScoreInline(laidTiles, boardSquares, true, true);
            extendedScore += CalculateExtendedScoreInline(laidTiles, boardSquares, false, true);
            extendedScore += CalculateExtendedScoreInline(laidTiles, boardSquares, true, false);
            extendedScore += CalculateExtendedScoreInline(laidTiles, boardSquares, false, false);

            foreach ((Square square, Tile tile) in laidTiles)
            {
                if (isLayingHorizontally)
                {
                    extendedScore += CalculateExtendedScorePerpendicular((square, tile), boardSquares, true, false);
                    extendedScore += CalculateExtendedScorePerpendicular((square, tile), boardSquares, false, false);
                }
                if (isLayingVertically)
                {
                    extendedScore += CalculateExtendedScorePerpendicular((square, tile), boardSquares, true, true);
                    extendedScore += CalculateExtendedScorePerpendicular((square, tile), boardSquares, false, true);
                }
                score += tile.Value * square.Type.LetterFactor;
                wordFactor = Math.Max(wordFactor, square.Type.WordFactor);
            }

            return (score * wordFactor) + extendedScore;
        }

        private static int CalculateExtendedScoreInline(IEnumerable<(Square Square, Tile Tile)> laidTiles, IEnumerable<Square> boardSquares, bool isPrefix, bool isHorizontal)
        {
            bool isLayingHorizontally = laidTiles.Any(t => t.Square.Point.X != laidTiles.First().Square.Point.X);
            bool isLayingVertically = laidTiles.Any(t => t.Square.Point.Y != laidTiles.First().Square.Point.Y);
            int score = 0;

            if ((isLayingHorizontally && !isHorizontal) || (isLayingVertically && isHorizontal))
            {
                return 0;
            }

            int directionFactor = 1;
            if (isPrefix)
            {
                directionFactor *= -1;
            }

            Func<Square, int> getHorizontalCoordinate = (Square square) => square.Point.X;
            Func<Square, int> getVerticalCoordinate = (Square square) => square.Point.Y;

            var getInlineCoordinate = isHorizontal ? getHorizontalCoordinate : getVerticalCoordinate;
            var getPerpendicularCoordinate = isHorizontal ? getVerticalCoordinate : getHorizontalCoordinate;

            var outermostSquare = laidTiles.Select(laidTile => laidTile.Square)
                .OrderBy(square => getInlineCoordinate(square) * directionFactor)
                .Last();

            while (boardSquares.Any(square => getInlineCoordinate(square) == getInlineCoordinate(outermostSquare) + directionFactor))
            {
                var square = boardSquares
                    .Where(x => x.State is Occupied)
                    .Where(x => getPerpendicularCoordinate(x) == getPerpendicularCoordinate(outermostSquare))
                    .SingleOrDefault(x => getInlineCoordinate(x) == getInlineCoordinate(outermostSquare) + directionFactor);

                if (square == null) break;
                var occupied = square.State as Occupied;
                outermostSquare = square;

                score += occupied.Tile.Value;
            }

            return score;
        }

        private static int CalculateExtendedScorePerpendicular((Square Square, Tile Tile) laidTile, IEnumerable<Square> boardSquares, bool isPrefix, bool isHorizontal)
        {
            bool extends = false;
            int score = 0;

            int directionFactor = 1;
            if (isPrefix)
            {
                directionFactor *= -1;
            }

            Func<Square, int> getHorizontalCoordinate = (Square square) => square.Point.X;
            Func<Square, int> getVerticalCoordinate = (Square square) => square.Point.Y;

            var getInlineCoordinate = isHorizontal ? getHorizontalCoordinate : getVerticalCoordinate;
            var getPerpendicularCoordinate = isHorizontal ? getVerticalCoordinate : getHorizontalCoordinate;


            while (boardSquares.Any(square => getInlineCoordinate(square) == getInlineCoordinate(laidTile.Square) + directionFactor))
            {
                var square = boardSquares
                    .Where(x => x.State is Occupied)
                    .Where(x => getPerpendicularCoordinate(x) == getPerpendicularCoordinate(laidTile.Square))
                    .SingleOrDefault(x => getInlineCoordinate(x) == getInlineCoordinate(laidTile.Square) + directionFactor);

                if (square == null) break;
                var occupied = square.State as Occupied;
                laidTile.Square = square;

                score += occupied.Tile.Value;
                extends = true;
            }

            if (extends) score += laidTile.Tile.Value;
            return score;
        }
    }
}