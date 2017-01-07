namespace InformationEngine
{
    public class X2
    {

        const int defX = 6, defY = 8;
        int[][] map = new int[50][];
        double[][] defMap = new double[defY + 1][];

        public X2()
        {
            for (int i = 0; i < 50; ++i)
                map[i] = new int[50];
            for (int i = 0; i < defY + 1; ++i)
                defMap[i] = new double[defX + 1];
        }

        public string Get(int width, int height, int[][] m)
        {
            map = m;
            judge(width, height);
            if (judge_1_1(0.9) > 0)
                return "1";
            if (judge_1_2(0.9) > 0)
                return "1";
            if (judge_2(0.9) > 0)
                return "2";
            if (judge_2_2(0.9) > 0)
                return "2";
            if (judge_7(0.7) > 0)
                return "7";
            if (judge_0(0.9) > 0)
                return "0";
            if (judge_3(0.9) > 0)
                return "3";
            if (judge_4(0.9) > 0)
                return "4";
            if (judge_5(0.9) > 0)
                return "5";
            if (judge_6(0.9) > 0)
                return "6";
            if (judge_8(0.9) > 0)
                return "8";
            return "0";
        }

        private void judge(int sizeX, int sizeY)
        {

            for (int orgY = 0; orgY < sizeY; orgY++)
            {
                for (int orgX = 0; orgX < sizeX; orgX++)
                {

                    double[] dx = new double[2] { orgX * defX * 1.0d / sizeX, (orgX + 1) * 1.0d * defX / sizeX };
                    int[] ix = new int[2];
                    ix[0] = (int)(dx[0]);
                    ix[1] = (int)(dx[1]);
                    double[] dy = new double[2] { orgY * defY * 1.0d / sizeY, (orgY + 1) * defY * 1.0d / sizeY };
                    int[] iy = new int[2];
                    iy[0] = (int)(dy[0]);
                    iy[1] = (int)(dy[1]);
                    if (ix[0] != ix[1])
                    {
                        dx[0] = ix[1] - dx[0];
                        dx[1] = dx[1] - ix[1];
                    }
                    else
                    {
                        double midx = (dx[1] + dx[0]) / 2;
                        dx[0] = midx - dx[0];
                        dx[1] = dx[1] - midx;
                    }
                    if (iy[0] != iy[1])
                    {
                        dy[0] = iy[1] - dy[0];
                        dy[1] = dy[1] - iy[1];
                    }
                    else
                    {
                        double midy = (dy[1] + dy[0]) / 2;
                        dy[0] = midy - dy[0];
                        dy[1] = dy[1] - midy;
                    }
                    defMap[iy[1]][ix[1]] += dx[1] * dy[1] * map[orgY][orgX];
                    defMap[iy[0]][ix[1]] += dx[1] * dy[0] * map[orgY][orgX];
                    defMap[iy[1]][ix[0]] += dx[0] * dy[1] * map[orgY][orgX];
                    defMap[iy[0]][ix[0]] += dx[0] * dy[0] * map[orgY][orgX];
                }
            }
        }

        private int judge_0(double pe)
        {
            if (defMap[3][0] + defMap[4][0] + defMap[5][0] < pe * 2) return 0;
            if (defMap[2][4] + defMap[3][4] + defMap[4][4] + defMap[5][4] + defMap[3][5] + defMap[4][5] < pe * 5.0) return 0;
            if (defMap[3][2] + defMap[4][2] + defMap[3][3] + defMap[4][3] > 12 * (1 - pe)) return 0;
            return 1;
        }

        private int judge_1_1(double pe)
        {
            if (defMap[1][2] + defMap[2][2] + defMap[3][2] + defMap[4][2] + defMap[5][2] + defMap[6][2] + defMap[7][2]
               + defMap[1][3] + defMap[2][3] + defMap[3][3] + defMap[4][3] + defMap[5][3] + defMap[6][3] + defMap[7][3] < 14 * pe) return 0;
            return 1;
        }

        private int judge_1_2(double pe)
        {
            if (defMap[0][4] + defMap[1][4] + defMap[2][4] + defMap[3][4] + defMap[4][4] + defMap[5][4] + defMap[6][4] + defMap[7][4] +
               defMap[0][5] + defMap[1][5] + defMap[2][5] + defMap[3][5] + defMap[4][5] + defMap[5][5] + defMap[6][5] + defMap[7][5] < 16 * pe) return 0;
            if (defMap[7][0] + defMap[4][0] + defMap[5][0] + defMap[6][0] > 0.4 * pe) return 0;
            return 1;
        }

        private int judge_2(double pe)
        {
            if (defMap[7][0] + defMap[7][1] + defMap[7][2] + defMap[7][3] + defMap[7][4] + defMap[6][2] < 6 * pe) return 0;
            if (defMap[0][2] + defMap[0][3] < 2 * pe) return 0;
            if (defMap[3][0] + defMap[4][0] + defMap[3][1] > 5 * (1 - pe)) return 0;
            return 1;
        }

        private int judge_2_2(double pe)
        {
            if (defMap[7][0] + defMap[7][1] + defMap[7][2] + defMap[7][3] + defMap[7][4] + defMap[7][5] < 5.5 * pe) return 0;
            if (defMap[6][0] + defMap[6][1] + defMap[6][2] + defMap[6][3] + defMap[6][4] + defMap[6][5] < 5.5 * pe) return 0;
            if (defMap[4][4] + defMap[5][4] > (1 - pe) * 2) return 0;
            if (defMap[1][0] + defMap[2][0] + defMap[1][1] + defMap[2][1] < 3.5 * pe) return 0;
            if (defMap[1][4] + defMap[2][4] < 1.8 * pe) return 0;
            return 1;
        }

        private int judge_3(double pe)
        {
            ///左中部，空
            if (defMap[3][0] + defMap[4][0] + defMap[4][1] > 7 * (1 - pe)) return 0;
            ///右边黑
            if (defMap[0][4] + defMap[1][4] + defMap[2][4] + defMap[3][4] + defMap[4][4] + defMap[5][4] + defMap[6][4] + defMap[7][4] < 7 * pe) return 0;
            ///上下八卦位黑
            if (defMap[0][2] + defMap[0][3] + defMap[7][2] + defMap[7][3] < 3.5 * pe) return 0;
            return 1;
        }

        private int judge_4(double pe)
        {
            if (defMap[0][0] + defMap[0][1] + defMap[1][0] + defMap[1][1] + defMap[0][2] > 4 * (1 - pe)) return 0;
            if (defMap[7][0] + defMap[7][1] > 3 * (1 - pe)) return 0;
            if (defMap[1][5] > 5 * (1 - pe)) return 0;
            return 1;
        }

        private int judge_5(double pe)
        {
            if (defMap[4][0] + defMap[4][1] + defMap[4][2] > 8 * (1 - pe)) return 0;
            if (defMap[2][3] + defMap[2][4] + defMap[2][5] > 6 * (1 - pe) &&
               defMap[1][3] + defMap[1][4] + defMap[1][5] > 8 * (1 - pe)) return 0;
            if ((defMap[0][1] + defMap[0][2] + defMap[0][3] + defMap[0][4] + defMap[0][5] + defMap[1][1]) < 5 * pe) return 0;
            return 1;
        }

        private int judge_6(double pe)
        {
            if (defMap[5][3] > 6 * (1 - pe)) return 0;

            if (defMap[2][1] + defMap[3][1] + defMap[4][1] + defMap[5][1] + defMap[6][1] + defMap[7][1] < 5 * pe) return 0;
            if (defMap[2][5] > 4 * (1 - pe)) return 0;
            return 1;
        }

        private int judge_7(double pe)
        {
            double sum = 0;
            for (int i = 4; i < 8; i++)
            {
                sum += defMap[i][0];
            }
            if (sum > (1 - pe)) return 0;
            sum = 0;
            if ((defMap[7][4] + defMap[7][5] + defMap[6][4] + defMap[6][5] + defMap[5][4] + defMap[5][5] + defMap[4][5]) > (1 - pe)) return 0;
            if ((defMap[0][1] + defMap[0][2] + defMap[0][3] + defMap[0][4] + defMap[0][5] + defMap[1][4]) < 6 * pe) return 0;
            return 1;
        }

        private int judge_8(double pe)
        {
            if (defMap[5][3] > 5 * (1 - pe)) return 0;
            if (defMap[1][4] + defMap[2][4] + defMap[3][4] + defMap[4][4] + defMap[5][4] + defMap[6][4] + defMap[7][4] < 7 * pe) return 0;
            if (defMap[1][1] + defMap[2][1] + defMap[3][1] + defMap[4][1] + defMap[5][1] + defMap[6][1] + defMap[7][1] < 6 * pe) return 0;
            if (defMap[3][1] + defMap[3][2] + defMap[3][3] + defMap[3][4] < 3 * pe) return 0;
            return 1;
        }

    }
}
