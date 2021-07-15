﻿//all of this code is heavily based on this video: https://www.youtube.com/watch?v=M3iI2l0ltbE&t=83s

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImGuiNET;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using SimplexNoise;
using YEET.Engine.Core;


namespace YEET.Engine.ECS
{
    public static class MarchingCubesTables
    {
        public static int[] cornerIndexBFromEdge = new int[] {1, 2, 3, 0, 5, 6, 7, 4, 4, 5, 6, 7};
        public static int[] cornerIndexAFromEdge = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3};

        public static int[] edges = new[]
        {
            0x0,
            0x109,
            0x203,
            0x30a,
            0x406,
            0x50f,
            0x605,
            0x70c,
            0x80c,
            0x905,
            0xa0f,
            0xb06,
            0xc0a,
            0xd03,
            0xe09,
            0xf00,
            0x190,
            0x99,
            0x393,
            0x29a,
            0x596,
            0x49f,
            0x795,
            0x69c,
            0x99c,
            0x895,
            0xb9f,
            0xa96,
            0xd9a,
            0xc93,
            0xf99,
            0xe90,
            0x230,
            0x339,
            0x33,
            0x13a,
            0x636,
            0x73f,
            0x435,
            0x53c,
            0xa3c,
            0xb35,
            0x83f,
            0x936,
            0xe3a,
            0xf33,
            0xc39,
            0xd30,
            0x3a0,
            0x2a9,
            0x1a3,
            0xaa,
            0x7a6,
            0x6af,
            0x5a5,
            0x4ac,
            0xbac,
            0xaa5,
            0x9af,
            0x8a6,
            0xfaa,
            0xea3,
            0xda9,
            0xca0,
            0x460,
            0x569,
            0x663,
            0x76a,
            0x66,
            0x16f,
            0x265,
            0x36c,
            0xc6c,
            0xd65,
            0xe6f,
            0xf66,
            0x86a,
            0x963,
            0xa69,
            0xb60,
            0x5f0,
            0x4f9,
            0x7f3,
            0x6fa,
            0x1f6,
            0xff,
            0x3f5,
            0x2fc,
            0xdfc,
            0xcf5,
            0xfff,
            0xef6,
            0x9fa,
            0x8f3,
            0xbf9,
            0xaf0,
            0x650,
            0x759,
            0x453,
            0x55a,
            0x256,
            0x35f,
            0x55,
            0x15c,
            0xe5c,
            0xf55,
            0xc5f,
            0xd56,
            0xa5a,
            0xb53,
            0x859,
            0x950,
            0x7c0,
            0x6c9,
            0x5c3,
            0x4ca,
            0x3c6,
            0x2cf,
            0x1c5,
            0xcc,
            0xfcc,
            0xec5,
            0xdcf,
            0xcc6,
            0xbca,
            0xac3,
            0x9c9,
            0x8c0,
            0x8c0,
            0x9c9,
            0xac3,
            0xbca,
            0xcc6,
            0xdcf,
            0xec5,
            0xfcc,
            0xcc,
            0x1c5,
            0x2cf,
            0x3c6,
            0x4ca,
            0x5c3,
            0x6c9,
            0x7c0,
            0x950,
            0x859,
            0xb53,
            0xa5a,
            0xd56,
            0xc5f,
            0xf55,
            0xe5c,
            0x15c,
            0x55,
            0x35f,
            0x256,
            0x55a,
            0x453,
            0x759,
            0x650,
            0xaf0,
            0xbf9,
            0x8f3,
            0x9fa,
            0xef6,
            0xfff,
            0xcf5,
            0xdfc,
            0x2fc,
            0x3f5,
            0xff,
            0x1f6,
            0x6fa,
            0x7f3,
            0x4f9,
            0x5f0,
            0xb60,
            0xa69,
            0x963,
            0x86a,
            0xf66,
            0xe6f,
            0xd65,
            0xc6c,
            0x36c,
            0x265,
            0x16f,
            0x66,
            0x76a,
            0x663,
            0x569,
            0x460,
            0xca0,
            0xda9,
            0xea3,
            0xfaa,
            0x8a6,
            0x9af,
            0xaa5,
            0xbac,
            0x4ac,
            0x5a5,
            0x6af,
            0x7a6,
            0xaa,
            0x1a3,
            0x2a9,
            0x3a0,
            0xd30,
            0xc39,
            0xf33,
            0xe3a,
            0x936,
            0x83f,
            0xb35,
            0xa3c,
            0x53c,
            0x435,
            0x73f,
            0x636,
            0x13a,
            0x33,
            0x339,
            0x230,
            0xe90,
            0xf99,
            0xc93,
            0xd9a,
            0xa96,
            0xb9f,
            0x895,
            0x99c,
            0x69c,
            0x795,
            0x49f,
            0x596,
            0x29a,
            0x393,
            0x99,
            0x190,
            0xf00,
            0xe09,
            0xd03,
            0xc0a,
            0xb06,
            0xa0f,
            0x905,
            0x80c,
            0x70c,
            0x605,
            0x50f,
            0x406,
            0x30a,
            0x203,
            0x109,
            0x0
        };


        public static int[,] triangulation = new int[,]
        {
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 1, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 8, 3, 9, 8, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 3, 1, 2, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {9, 2, 10, 0, 2, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {2, 8, 3, 2, 10, 8, 10, 9, 8, -1, -1, -1, -1, -1, -1, -1},
            {3, 11, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 11, 2, 8, 11, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 9, 0, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 11, 2, 1, 9, 11, 9, 8, 11, -1, -1, -1, -1, -1, -1, -1},
            {3, 10, 1, 11, 10, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 10, 1, 0, 8, 10, 8, 11, 10, -1, -1, -1, -1, -1, -1, -1},
            {3, 9, 0, 3, 11, 9, 11, 10, 9, -1, -1, -1, -1, -1, -1, -1},
            {9, 8, 10, 10, 8, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 7, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 3, 0, 7, 3, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 1, 9, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 1, 9, 4, 7, 1, 7, 3, 1, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 10, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {3, 4, 7, 3, 0, 4, 1, 2, 10, -1, -1, -1, -1, -1, -1, -1},
            {9, 2, 10, 9, 0, 2, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1},
            {2, 10, 9, 2, 9, 7, 2, 7, 3, 7, 9, 4, -1, -1, -1, -1},
            {8, 4, 7, 3, 11, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {11, 4, 7, 11, 2, 4, 2, 0, 4, -1, -1, -1, -1, -1, -1, -1},
            {9, 0, 1, 8, 4, 7, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1},
            {4, 7, 11, 9, 4, 11, 9, 11, 2, 9, 2, 1, -1, -1, -1, -1},
            {3, 10, 1, 3, 11, 10, 7, 8, 4, -1, -1, -1, -1, -1, -1, -1},
            {1, 11, 10, 1, 4, 11, 1, 0, 4, 7, 11, 4, -1, -1, -1, -1},
            {4, 7, 8, 9, 0, 11, 9, 11, 10, 11, 0, 3, -1, -1, -1, -1},
            {4, 7, 11, 4, 11, 9, 9, 11, 10, -1, -1, -1, -1, -1, -1, -1},
            {9, 5, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {9, 5, 4, 0, 8, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 5, 4, 1, 5, 0, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {8, 5, 4, 8, 3, 5, 3, 1, 5, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 10, 9, 5, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {3, 0, 8, 1, 2, 10, 4, 9, 5, -1, -1, -1, -1, -1, -1, -1},
            {5, 2, 10, 5, 4, 2, 4, 0, 2, -1, -1, -1, -1, -1, -1, -1},
            {2, 10, 5, 3, 2, 5, 3, 5, 4, 3, 4, 8, -1, -1, -1, -1},
            {9, 5, 4, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 11, 2, 0, 8, 11, 4, 9, 5, -1, -1, -1, -1, -1, -1, -1},
            {0, 5, 4, 0, 1, 5, 2, 3, 11, -1, -1, -1, -1, -1, -1, -1},
            {2, 1, 5, 2, 5, 8, 2, 8, 11, 4, 8, 5, -1, -1, -1, -1},
            {10, 3, 11, 10, 1, 3, 9, 5, 4, -1, -1, -1, -1, -1, -1, -1},
            {4, 9, 5, 0, 8, 1, 8, 10, 1, 8, 11, 10, -1, -1, -1, -1},
            {5, 4, 0, 5, 0, 11, 5, 11, 10, 11, 0, 3, -1, -1, -1, -1},
            {5, 4, 8, 5, 8, 10, 10, 8, 11, -1, -1, -1, -1, -1, -1, -1},
            {9, 7, 8, 5, 7, 9, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {9, 3, 0, 9, 5, 3, 5, 7, 3, -1, -1, -1, -1, -1, -1, -1},
            {0, 7, 8, 0, 1, 7, 1, 5, 7, -1, -1, -1, -1, -1, -1, -1},
            {1, 5, 3, 3, 5, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {9, 7, 8, 9, 5, 7, 10, 1, 2, -1, -1, -1, -1, -1, -1, -1},
            {10, 1, 2, 9, 5, 0, 5, 3, 0, 5, 7, 3, -1, -1, -1, -1},
            {8, 0, 2, 8, 2, 5, 8, 5, 7, 10, 5, 2, -1, -1, -1, -1},
            {2, 10, 5, 2, 5, 3, 3, 5, 7, -1, -1, -1, -1, -1, -1, -1},
            {7, 9, 5, 7, 8, 9, 3, 11, 2, -1, -1, -1, -1, -1, -1, -1},
            {9, 5, 7, 9, 7, 2, 9, 2, 0, 2, 7, 11, -1, -1, -1, -1},
            {2, 3, 11, 0, 1, 8, 1, 7, 8, 1, 5, 7, -1, -1, -1, -1},
            {11, 2, 1, 11, 1, 7, 7, 1, 5, -1, -1, -1, -1, -1, -1, -1},
            {9, 5, 8, 8, 5, 7, 10, 1, 3, 10, 3, 11, -1, -1, -1, -1},
            {5, 7, 0, 5, 0, 9, 7, 11, 0, 1, 0, 10, 11, 10, 0, -1},
            {11, 10, 0, 11, 0, 3, 10, 5, 0, 8, 0, 7, 5, 7, 0, -1},
            {11, 10, 5, 7, 11, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {10, 6, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 3, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {9, 0, 1, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 8, 3, 1, 9, 8, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1},
            {1, 6, 5, 2, 6, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 6, 5, 1, 2, 6, 3, 0, 8, -1, -1, -1, -1, -1, -1, -1},
            {9, 6, 5, 9, 0, 6, 0, 2, 6, -1, -1, -1, -1, -1, -1, -1},
            {5, 9, 8, 5, 8, 2, 5, 2, 6, 3, 2, 8, -1, -1, -1, -1},
            {2, 3, 11, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {11, 0, 8, 11, 2, 0, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1},
            {0, 1, 9, 2, 3, 11, 5, 10, 6, -1, -1, -1, -1, -1, -1, -1},
            {5, 10, 6, 1, 9, 2, 9, 11, 2, 9, 8, 11, -1, -1, -1, -1},
            {6, 3, 11, 6, 5, 3, 5, 1, 3, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 11, 0, 11, 5, 0, 5, 1, 5, 11, 6, -1, -1, -1, -1},
            {3, 11, 6, 0, 3, 6, 0, 6, 5, 0, 5, 9, -1, -1, -1, -1},
            {6, 5, 9, 6, 9, 11, 11, 9, 8, -1, -1, -1, -1, -1, -1, -1},
            {5, 10, 6, 4, 7, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 3, 0, 4, 7, 3, 6, 5, 10, -1, -1, -1, -1, -1, -1, -1},
            {1, 9, 0, 5, 10, 6, 8, 4, 7, -1, -1, -1, -1, -1, -1, -1},
            {10, 6, 5, 1, 9, 7, 1, 7, 3, 7, 9, 4, -1, -1, -1, -1},
            {6, 1, 2, 6, 5, 1, 4, 7, 8, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 5, 5, 2, 6, 3, 0, 4, 3, 4, 7, -1, -1, -1, -1},
            {8, 4, 7, 9, 0, 5, 0, 6, 5, 0, 2, 6, -1, -1, -1, -1},
            {7, 3, 9, 7, 9, 4, 3, 2, 9, 5, 9, 6, 2, 6, 9, -1},
            {3, 11, 2, 7, 8, 4, 10, 6, 5, -1, -1, -1, -1, -1, -1, -1},
            {5, 10, 6, 4, 7, 2, 4, 2, 0, 2, 7, 11, -1, -1, -1, -1},
            {0, 1, 9, 4, 7, 8, 2, 3, 11, 5, 10, 6, -1, -1, -1, -1},
            {9, 2, 1, 9, 11, 2, 9, 4, 11, 7, 11, 4, 5, 10, 6, -1},
            {8, 4, 7, 3, 11, 5, 3, 5, 1, 5, 11, 6, -1, -1, -1, -1},
            {5, 1, 11, 5, 11, 6, 1, 0, 11, 7, 11, 4, 0, 4, 11, -1},
            {0, 5, 9, 0, 6, 5, 0, 3, 6, 11, 6, 3, 8, 4, 7, -1},
            {6, 5, 9, 6, 9, 11, 4, 7, 9, 7, 11, 9, -1, -1, -1, -1},
            {10, 4, 9, 6, 4, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 10, 6, 4, 9, 10, 0, 8, 3, -1, -1, -1, -1, -1, -1, -1},
            {10, 0, 1, 10, 6, 0, 6, 4, 0, -1, -1, -1, -1, -1, -1, -1},
            {8, 3, 1, 8, 1, 6, 8, 6, 4, 6, 1, 10, -1, -1, -1, -1},
            {1, 4, 9, 1, 2, 4, 2, 6, 4, -1, -1, -1, -1, -1, -1, -1},
            {3, 0, 8, 1, 2, 9, 2, 4, 9, 2, 6, 4, -1, -1, -1, -1},
            {0, 2, 4, 4, 2, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {8, 3, 2, 8, 2, 4, 4, 2, 6, -1, -1, -1, -1, -1, -1, -1},
            {10, 4, 9, 10, 6, 4, 11, 2, 3, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 2, 2, 8, 11, 4, 9, 10, 4, 10, 6, -1, -1, -1, -1},
            {3, 11, 2, 0, 1, 6, 0, 6, 4, 6, 1, 10, -1, -1, -1, -1},
            {6, 4, 1, 6, 1, 10, 4, 8, 1, 2, 1, 11, 8, 11, 1, -1},
            {9, 6, 4, 9, 3, 6, 9, 1, 3, 11, 6, 3, -1, -1, -1, -1},
            {8, 11, 1, 8, 1, 0, 11, 6, 1, 9, 1, 4, 6, 4, 1, -1},
            {3, 11, 6, 3, 6, 0, 0, 6, 4, -1, -1, -1, -1, -1, -1, -1},
            {6, 4, 8, 11, 6, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {7, 10, 6, 7, 8, 10, 8, 9, 10, -1, -1, -1, -1, -1, -1, -1},
            {0, 7, 3, 0, 10, 7, 0, 9, 10, 6, 7, 10, -1, -1, -1, -1},
            {10, 6, 7, 1, 10, 7, 1, 7, 8, 1, 8, 0, -1, -1, -1, -1},
            {10, 6, 7, 10, 7, 1, 1, 7, 3, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 6, 1, 6, 8, 1, 8, 9, 8, 6, 7, -1, -1, -1, -1},
            {2, 6, 9, 2, 9, 1, 6, 7, 9, 0, 9, 3, 7, 3, 9, -1},
            {7, 8, 0, 7, 0, 6, 6, 0, 2, -1, -1, -1, -1, -1, -1, -1},
            {7, 3, 2, 6, 7, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {2, 3, 11, 10, 6, 8, 10, 8, 9, 8, 6, 7, -1, -1, -1, -1},
            {2, 0, 7, 2, 7, 11, 0, 9, 7, 6, 7, 10, 9, 10, 7, -1},
            {1, 8, 0, 1, 7, 8, 1, 10, 7, 6, 7, 10, 2, 3, 11, -1},
            {11, 2, 1, 11, 1, 7, 10, 6, 1, 6, 7, 1, -1, -1, -1, -1},
            {8, 9, 6, 8, 6, 7, 9, 1, 6, 11, 6, 3, 1, 3, 6, -1},
            {0, 9, 1, 11, 6, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {7, 8, 0, 7, 0, 6, 3, 11, 0, 11, 6, 0, -1, -1, -1, -1},
            {7, 11, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {7, 6, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {3, 0, 8, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 1, 9, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {8, 1, 9, 8, 3, 1, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1},
            {10, 1, 2, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 10, 3, 0, 8, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1},
            {2, 9, 0, 2, 10, 9, 6, 11, 7, -1, -1, -1, -1, -1, -1, -1},
            {6, 11, 7, 2, 10, 3, 10, 8, 3, 10, 9, 8, -1, -1, -1, -1},
            {7, 2, 3, 6, 2, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {7, 0, 8, 7, 6, 0, 6, 2, 0, -1, -1, -1, -1, -1, -1, -1},
            {2, 7, 6, 2, 3, 7, 0, 1, 9, -1, -1, -1, -1, -1, -1, -1},
            {1, 6, 2, 1, 8, 6, 1, 9, 8, 8, 7, 6, -1, -1, -1, -1},
            {10, 7, 6, 10, 1, 7, 1, 3, 7, -1, -1, -1, -1, -1, -1, -1},
            {10, 7, 6, 1, 7, 10, 1, 8, 7, 1, 0, 8, -1, -1, -1, -1},
            {0, 3, 7, 0, 7, 10, 0, 10, 9, 6, 10, 7, -1, -1, -1, -1},
            {7, 6, 10, 7, 10, 8, 8, 10, 9, -1, -1, -1, -1, -1, -1, -1},
            {6, 8, 4, 11, 8, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {3, 6, 11, 3, 0, 6, 0, 4, 6, -1, -1, -1, -1, -1, -1, -1},
            {8, 6, 11, 8, 4, 6, 9, 0, 1, -1, -1, -1, -1, -1, -1, -1},
            {9, 4, 6, 9, 6, 3, 9, 3, 1, 11, 3, 6, -1, -1, -1, -1},
            {6, 8, 4, 6, 11, 8, 2, 10, 1, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 10, 3, 0, 11, 0, 6, 11, 0, 4, 6, -1, -1, -1, -1},
            {4, 11, 8, 4, 6, 11, 0, 2, 9, 2, 10, 9, -1, -1, -1, -1},
            {10, 9, 3, 10, 3, 2, 9, 4, 3, 11, 3, 6, 4, 6, 3, -1},
            {8, 2, 3, 8, 4, 2, 4, 6, 2, -1, -1, -1, -1, -1, -1, -1},
            {0, 4, 2, 4, 6, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 9, 0, 2, 3, 4, 2, 4, 6, 4, 3, 8, -1, -1, -1, -1},
            {1, 9, 4, 1, 4, 2, 2, 4, 6, -1, -1, -1, -1, -1, -1, -1},
            {8, 1, 3, 8, 6, 1, 8, 4, 6, 6, 10, 1, -1, -1, -1, -1},
            {10, 1, 0, 10, 0, 6, 6, 0, 4, -1, -1, -1, -1, -1, -1, -1},
            {4, 6, 3, 4, 3, 8, 6, 10, 3, 0, 3, 9, 10, 9, 3, -1},
            {10, 9, 4, 6, 10, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 9, 5, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 3, 4, 9, 5, 11, 7, 6, -1, -1, -1, -1, -1, -1, -1},
            {5, 0, 1, 5, 4, 0, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1},
            {11, 7, 6, 8, 3, 4, 3, 5, 4, 3, 1, 5, -1, -1, -1, -1},
            {9, 5, 4, 10, 1, 2, 7, 6, 11, -1, -1, -1, -1, -1, -1, -1},
            {6, 11, 7, 1, 2, 10, 0, 8, 3, 4, 9, 5, -1, -1, -1, -1},
            {7, 6, 11, 5, 4, 10, 4, 2, 10, 4, 0, 2, -1, -1, -1, -1},
            {3, 4, 8, 3, 5, 4, 3, 2, 5, 10, 5, 2, 11, 7, 6, -1},
            {7, 2, 3, 7, 6, 2, 5, 4, 9, -1, -1, -1, -1, -1, -1, -1},
            {9, 5, 4, 0, 8, 6, 0, 6, 2, 6, 8, 7, -1, -1, -1, -1},
            {3, 6, 2, 3, 7, 6, 1, 5, 0, 5, 4, 0, -1, -1, -1, -1},
            {6, 2, 8, 6, 8, 7, 2, 1, 8, 4, 8, 5, 1, 5, 8, -1},
            {9, 5, 4, 10, 1, 6, 1, 7, 6, 1, 3, 7, -1, -1, -1, -1},
            {1, 6, 10, 1, 7, 6, 1, 0, 7, 8, 7, 0, 9, 5, 4, -1},
            {4, 0, 10, 4, 10, 5, 0, 3, 10, 6, 10, 7, 3, 7, 10, -1},
            {7, 6, 10, 7, 10, 8, 5, 4, 10, 4, 8, 10, -1, -1, -1, -1},
            {6, 9, 5, 6, 11, 9, 11, 8, 9, -1, -1, -1, -1, -1, -1, -1},
            {3, 6, 11, 0, 6, 3, 0, 5, 6, 0, 9, 5, -1, -1, -1, -1},
            {0, 11, 8, 0, 5, 11, 0, 1, 5, 5, 6, 11, -1, -1, -1, -1},
            {6, 11, 3, 6, 3, 5, 5, 3, 1, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 10, 9, 5, 11, 9, 11, 8, 11, 5, 6, -1, -1, -1, -1},
            {0, 11, 3, 0, 6, 11, 0, 9, 6, 5, 6, 9, 1, 2, 10, -1},
            {11, 8, 5, 11, 5, 6, 8, 0, 5, 10, 5, 2, 0, 2, 5, -1},
            {6, 11, 3, 6, 3, 5, 2, 10, 3, 10, 5, 3, -1, -1, -1, -1},
            {5, 8, 9, 5, 2, 8, 5, 6, 2, 3, 8, 2, -1, -1, -1, -1},
            {9, 5, 6, 9, 6, 0, 0, 6, 2, -1, -1, -1, -1, -1, -1, -1},
            {1, 5, 8, 1, 8, 0, 5, 6, 8, 3, 8, 2, 6, 2, 8, -1},
            {1, 5, 6, 2, 1, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 3, 6, 1, 6, 10, 3, 8, 6, 5, 6, 9, 8, 9, 6, -1},
            {10, 1, 0, 10, 0, 6, 9, 5, 0, 5, 6, 0, -1, -1, -1, -1},
            {0, 3, 8, 5, 6, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {10, 5, 6, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {11, 5, 10, 7, 5, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {11, 5, 10, 11, 7, 5, 8, 3, 0, -1, -1, -1, -1, -1, -1, -1},
            {5, 11, 7, 5, 10, 11, 1, 9, 0, -1, -1, -1, -1, -1, -1, -1},
            {10, 7, 5, 10, 11, 7, 9, 8, 1, 8, 3, 1, -1, -1, -1, -1},
            {11, 1, 2, 11, 7, 1, 7, 5, 1, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 3, 1, 2, 7, 1, 7, 5, 7, 2, 11, -1, -1, -1, -1},
            {9, 7, 5, 9, 2, 7, 9, 0, 2, 2, 11, 7, -1, -1, -1, -1},
            {7, 5, 2, 7, 2, 11, 5, 9, 2, 3, 2, 8, 9, 8, 2, -1},
            {2, 5, 10, 2, 3, 5, 3, 7, 5, -1, -1, -1, -1, -1, -1, -1},
            {8, 2, 0, 8, 5, 2, 8, 7, 5, 10, 2, 5, -1, -1, -1, -1},
            {9, 0, 1, 5, 10, 3, 5, 3, 7, 3, 10, 2, -1, -1, -1, -1},
            {9, 8, 2, 9, 2, 1, 8, 7, 2, 10, 2, 5, 7, 5, 2, -1},
            {1, 3, 5, 3, 7, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 7, 0, 7, 1, 1, 7, 5, -1, -1, -1, -1, -1, -1, -1},
            {9, 0, 3, 9, 3, 5, 5, 3, 7, -1, -1, -1, -1, -1, -1, -1},
            {9, 8, 7, 5, 9, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {5, 8, 4, 5, 10, 8, 10, 11, 8, -1, -1, -1, -1, -1, -1, -1},
            {5, 0, 4, 5, 11, 0, 5, 10, 11, 11, 3, 0, -1, -1, -1, -1},
            {0, 1, 9, 8, 4, 10, 8, 10, 11, 10, 4, 5, -1, -1, -1, -1},
            {10, 11, 4, 10, 4, 5, 11, 3, 4, 9, 4, 1, 3, 1, 4, -1},
            {2, 5, 1, 2, 8, 5, 2, 11, 8, 4, 5, 8, -1, -1, -1, -1},
            {0, 4, 11, 0, 11, 3, 4, 5, 11, 2, 11, 1, 5, 1, 11, -1},
            {0, 2, 5, 0, 5, 9, 2, 11, 5, 4, 5, 8, 11, 8, 5, -1},
            {9, 4, 5, 2, 11, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {2, 5, 10, 3, 5, 2, 3, 4, 5, 3, 8, 4, -1, -1, -1, -1},
            {5, 10, 2, 5, 2, 4, 4, 2, 0, -1, -1, -1, -1, -1, -1, -1},
            {3, 10, 2, 3, 5, 10, 3, 8, 5, 4, 5, 8, 0, 1, 9, -1},
            {5, 10, 2, 5, 2, 4, 1, 9, 2, 9, 4, 2, -1, -1, -1, -1},
            {8, 4, 5, 8, 5, 3, 3, 5, 1, -1, -1, -1, -1, -1, -1, -1},
            {0, 4, 5, 1, 0, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {8, 4, 5, 8, 5, 3, 9, 0, 5, 0, 3, 5, -1, -1, -1, -1},
            {9, 4, 5, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 11, 7, 4, 9, 11, 9, 10, 11, -1, -1, -1, -1, -1, -1, -1},
            {0, 8, 3, 4, 9, 7, 9, 11, 7, 9, 10, 11, -1, -1, -1, -1},
            {1, 10, 11, 1, 11, 4, 1, 4, 0, 7, 4, 11, -1, -1, -1, -1},
            {3, 1, 4, 3, 4, 8, 1, 10, 4, 7, 4, 11, 10, 11, 4, -1},
            {4, 11, 7, 9, 11, 4, 9, 2, 11, 9, 1, 2, -1, -1, -1, -1},
            {9, 7, 4, 9, 11, 7, 9, 1, 11, 2, 11, 1, 0, 8, 3, -1},
            {11, 7, 4, 11, 4, 2, 2, 4, 0, -1, -1, -1, -1, -1, -1, -1},
            {11, 7, 4, 11, 4, 2, 8, 3, 4, 3, 2, 4, -1, -1, -1, -1},
            {2, 9, 10, 2, 7, 9, 2, 3, 7, 7, 4, 9, -1, -1, -1, -1},
            {9, 10, 7, 9, 7, 4, 10, 2, 7, 8, 7, 0, 2, 0, 7, -1},
            {3, 7, 10, 3, 10, 2, 7, 4, 10, 1, 10, 0, 4, 0, 10, -1},
            {1, 10, 2, 8, 7, 4, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 9, 1, 4, 1, 7, 7, 1, 3, -1, -1, -1, -1, -1, -1, -1},
            {4, 9, 1, 4, 1, 7, 0, 8, 1, 8, 7, 1, -1, -1, -1, -1},
            {4, 0, 3, 7, 4, 3, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {4, 8, 7, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {9, 10, 8, 10, 11, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {3, 0, 9, 3, 9, 11, 11, 9, 10, -1, -1, -1, -1, -1, -1, -1},
            {0, 1, 10, 0, 10, 8, 8, 10, 11, -1, -1, -1, -1, -1, -1, -1},
            {3, 1, 10, 11, 3, 10, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 2, 11, 1, 11, 9, 9, 11, 8, -1, -1, -1, -1, -1, -1, -1},
            {3, 0, 9, 3, 9, 11, 1, 2, 9, 2, 11, 9, -1, -1, -1, -1},
            {0, 2, 11, 8, 0, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {3, 2, 11, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {2, 3, 8, 2, 8, 10, 10, 8, 9, -1, -1, -1, -1, -1, -1, -1},
            {9, 10, 2, 0, 9, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {2, 3, 8, 2, 8, 10, 0, 1, 8, 1, 10, 8, -1, -1, -1, -1},
            {1, 10, 2, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {1, 3, 8, 9, 1, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 9, 1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {0, 3, 8, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1},
            {-1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1}
        };
    }
    public class MarchingCubeTerrain : Entity
    {
        private List<MarchingCubeChunk> _chunks;
        public float Scale = 0.019f;
        public int Divider = 7;
        public float SurfaceLevel = 0.319f;
        public Vector3 Dimensions;
        public int Seed;
        private ShaderLoader _loader;

        private float lastsurface = 0, lastscale = 0;
        private int lastSeed = 0, lastDivider = 0;

        Guid mesh;

        public MarchingCubeTerrain(Vector3 _Dimensions, Vector3i origin)
        {
            _chunks = new List<MarchingCubeChunk>();
            Dimensions = _Dimensions;
            var rand = new Random();
            Seed = rand.Next();
            _loader = new ShaderLoader("MarchingCubes");
            mesh = AddComponent(new Mesh(this,_loader));
            _chunks.Clear();
            for (int x = 0; x < Dimensions.X; x++)
            {
                for (int y = 0; y < Dimensions.Y; y++)
                {
                    for (int z = 0; z < Dimensions.Z; z++)
                    {
                        _chunks.Add(new MarchingCubeChunk(Seed, new Vector3i((x * 32), (y * 32), (z * 32)), _loader));
                    }
                }
            }

            Generate();
        }


        public override void OnUpdate()
        {
            base.OnUpdate();
            Generate();
        }

        public override void OnRender()
        {
            base.OnRender();
            foreach (var chunk in _chunks)
            {
                chunk.OnDraw();
            }
        }


        public override void OnGui()
        {
            if (ShowGUI)
            {
                ImGui.Begin("Terrain (MC)");
                ImGui.SetWindowFontScale(1.5f);
                base.OnGui();
                ImGui.Checkbox("Active", ref Active);
                ImGui.SliderFloat("Scale", ref Scale, 0.001f, 0.1f);
                ImGui.SliderFloat("Surface Level", ref SurfaceLevel, 0.01f, 1.0f);
                ImGui.SliderInt("Divider", ref Divider, 1, 30);
                ImGui.End();
            }

            ImGui.Checkbox("Terrain", ref ShowGUI);
            
        }

        void Generate()
        {
            if (lastscale != Scale || lastsurface != SurfaceLevel || lastDivider != Divider || lastSeed != Seed)
            {
                var watch = new Util.StopWatchMilliseconds();
                lastscale = Scale;
                lastsurface = SurfaceLevel;
                lastDivider = Divider;
                lastSeed = Seed;
                foreach (var chunk in _chunks)
                {
                    chunk.Scale = Scale;
                    chunk.Divider = Divider;
                    chunk.SurfaceLevel = SurfaceLevel;
                    var x =GetComponent<Transform>().Position;
                    chunk.Generate(new Vector3i(Convert.ToInt32(x.X),Convert.ToInt32(x.Y),Convert.ToInt32(x.Z)));
                }

                Console.WriteLine("Generated " + _chunks.Count + " Chunks.Took: " + watch.Result() + "ms");
            }
        }
    }


    class MarchingCubeChunk
    {
        private List<Vector3> FinalTriangleVertices;
        private List<Vector3> PointsVisualizeCoordinates;
        public float Scale = 0.019f;
        public ShaderLoader Loader;
        private int VAO, VBO;
        private float lastscale, lastsurface = 0.1f;
        public int Dimension = 32;
        public float SurfaceLevel = 0.319f;
        public Vector3i Offset;
        public bool NeedsUpdate = true;
        public float Divider = 7;
        private float last_divider = 100;

        

        private Vector3[] cubeCornerOffsets =
        {
            new(0, 0, 0),
            new(1, 0, 0),
            new(1, 0, 1),
            new(0, 0, 1),
            new(0, 1, 0),
            new(1, 1, 0),
            new(1, 1, 1),
            new(0, 1, 1),
        };

        public MarchingCubeChunk(int noise_seed, Vector3i offset, ShaderLoader loader)
        {
            FinalTriangleVertices = new List<Vector3>();
            PointsVisualizeCoordinates = new List<Vector3>();
            Loader = loader;
            VAO = GL.GenVertexArray();
            VBO = GL.GenBuffer();

            Offset = offset;
            Noise.Seed = noise_seed;
        }

        public void Generate(Vector3i origin)
        {
            if (lastscale != Scale || lastsurface != SurfaceLevel || NeedsUpdate || last_divider != Divider)
            {
                NeedsUpdate = false;
                last_divider = Divider;
                lastscale = Scale;
                lastsurface = SurfaceLevel;
                Util.StopWatchMilliseconds watch = new Util.StopWatchMilliseconds();
                FinalTriangleVertices.Clear();
                
                Parallel.For(0, Dimension,
                    x =>
                    {
                        Parallel.For(0, Dimension, y =>
                        {
                            Parallel.For(0, Dimension, z =>
                            {
                                March((x+origin.X, y+origin.Y, z+origin.Z));
                            });
                        });
                    });

                GL.BindVertexArray(VAO);
                GL.BindBuffer(BufferTarget.ArrayBuffer, VBO);
                GL.BufferData(BufferTarget.ArrayBuffer, FinalTriangleVertices.Count * 3 * sizeof(float),
                    FinalTriangleVertices.ToArray(),
                    BufferUsageHint.DynamicDraw);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 9 * sizeof(float), 0);
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(1, 3, VertexAttribPointerType.Float, false, 9 * sizeof(float),
                    3 * sizeof(float));
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 9 * sizeof(float),
                    6 * sizeof(float));
                GL.EnableVertexAttribArray(2);
                GL.BindVertexArray(0);

                
            
                Console.WriteLine("Generated Marching Cube.Offset:" + Offset + " Dimensions:" + Dimension + "x" +
                                  Dimension + "x" +
                                  Dimension + " Took:" + watch.Result() + "ms");
            }
        }
        

        public void OnDraw()
        {
            Loader.UseShader();
            GL.BindVertexArray(VAO);
            Loader.SetUniformMatrix4F("view", Camera.View);
            Loader.SetUniformMatrix4F("projection", Camera.Projection);
            //Loader.SetUniformVec3("offset", Offset);
            GL.DrawArrays(PrimitiveType.Triangles, 0, FinalTriangleVertices.Count / 3);

            GL.BindVertexArray(0);
        }

        public float GenerateFloatPerPoint(Vector3i pos)
        {
            pos = pos + Offset;
            return -pos.Y + (Noise.CalcPixel3D(pos.X, pos.Y, pos.Z, Scale) / Divider);
        }

        public float GenerateFloatPerPointF(Vector3 pos)
        {
            pos = pos + Offset;
            return -pos.Y + (Noise.CalcPixel3D(Convert.ToInt32(pos.X), Convert.ToInt32(pos.Y), Convert.ToInt32(pos.Z), Scale) / Divider);
        }
        
        public int GetSurface(Vector2i pos)
        {
            for (int x = 0; x < Dimension; x++)
            {
                if (GenerateFloatPerPoint(new Vector3i(pos.X, x, pos.Y)) < SurfaceLevel)
                    return x;
            }

            return -1;
        }

        
        Vector3 interpolateVerts(Vector3 v1, Vector3 v2) {
            float t = (SurfaceLevel - GenerateFloatPerPointF(v1)) / (GenerateFloatPerPointF(v2) - GenerateFloatPerPointF(v1));
            return v1 + t * (v2-v1);
        }
        

        private void March(Vector3i CubePosition)
        {
            float[] cubeCorners =
            {
                GenerateFloatPerPoint((CubePosition.X, CubePosition.Y, CubePosition.Z)),
                GenerateFloatPerPoint((CubePosition.X + 1, CubePosition.Y, CubePosition.Z)),
                GenerateFloatPerPoint((CubePosition.X + 1, CubePosition.Y, CubePosition.Z + 1)),
                GenerateFloatPerPoint((CubePosition.X, CubePosition.Y, CubePosition.Z + 1)),
                GenerateFloatPerPoint((CubePosition.X, CubePosition.Y + 1, CubePosition.Z)),
                GenerateFloatPerPoint((CubePosition.X + 1, CubePosition.Y + 1, CubePosition.Z)),
                GenerateFloatPerPoint((CubePosition.X + 1, CubePosition.Y + 1, CubePosition.Z + 1)),
                GenerateFloatPerPoint((CubePosition.X, CubePosition.Y + 1, CubePosition.Z + 1))
            };
            
            int cubeIndex = 0;
            if (cubeCorners[0] < SurfaceLevel) cubeIndex |= 1;
            if (cubeCorners[1] < SurfaceLevel) cubeIndex |= 2;
            if (cubeCorners[2] < SurfaceLevel) cubeIndex |= 4;
            if (cubeCorners[3] < SurfaceLevel) cubeIndex |= 8;
            if (cubeCorners[4] < SurfaceLevel) cubeIndex |= 16;
            if (cubeCorners[5] < SurfaceLevel) cubeIndex |= 32;
            if (cubeCorners[6] < SurfaceLevel) cubeIndex |= 64;
            if (cubeCorners[7] < SurfaceLevel) cubeIndex |= 128;


            for (int i = 0; MarchingCubesTables.triangulation[cubeIndex, i] != -1; i += 3)
            {
                // Get indices of corner points A and B for each of the three edges
                // of the cube that need to be joined to form the triangle.
                int a0 = MarchingCubesTables.cornerIndexAFromEdge[MarchingCubesTables.triangulation[cubeIndex, i]];
                int b0 = MarchingCubesTables.cornerIndexBFromEdge[MarchingCubesTables.triangulation[cubeIndex, i]];

                int a1 = MarchingCubesTables.cornerIndexAFromEdge[MarchingCubesTables.triangulation[cubeIndex, i + 1]];
                int b1 = MarchingCubesTables.cornerIndexBFromEdge[MarchingCubesTables.triangulation[cubeIndex, i + 1]];

                int a2 = MarchingCubesTables.cornerIndexAFromEdge[MarchingCubesTables.triangulation[cubeIndex, i + 2]];
                int b2 = MarchingCubesTables.cornerIndexBFromEdge[MarchingCubesTables.triangulation[cubeIndex, i + 2]];

                lock (FinalTriangleVertices)
                {
                    Vector3 Color = (0, 0.6f, 0.098f);
                    //Vector3 Color = new Vector3(Noise.CalcPixel1D(CubePosition.X,Scale)/255f,Noise.CalcPixel1D(CubePosition.Y,Scale)/255f,
                    //    Noise.CalcPixel1D(CubePosition.Z,Scale)/255f);
                    
                    Vector3 v1 = ((cubeCornerOffsets[a0] + CubePosition)+(cubeCornerOffsets[b0] + CubePosition))/2;//interpolateVerts((cubeCornerOffsets[a0] + CubePosition), (cubeCornerOffsets[b0] + CubePosition));
                    Vector3 v2 = ((cubeCornerOffsets[a1] + CubePosition)+(cubeCornerOffsets[b1] + CubePosition))/2;//interpolateVerts((cubeCornerOffsets[a1] + CubePosition), (cubeCornerOffsets[b1] + CubePosition));
                    Vector3 v3 = ((cubeCornerOffsets[a2] + CubePosition)+(cubeCornerOffsets[b2] + CubePosition))/2;//interpolateVerts((cubeCornerOffsets[a2] + CubePosition), (cubeCornerOffsets[b2] + CubePosition));
                    var dir = Vector3.Cross(v2 - v1, v3 - v1);
                    Vector3 norm = -Vector3.Normalize(dir);
                    
                    lock (FinalTriangleVertices)
                    {
                        FinalTriangleVertices.AddRange(new List<Vector3>()
                            {v1+Offset, Color, norm, v2+Offset, Color, norm, v3+Offset, Color, norm});
                    }
                }
            }
        }
    }
}