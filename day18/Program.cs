using System.Numerics;

StreamReader reader = new(File.OpenRead("./day18/input.txt")); 

int cTrench1 = 0;
List<Tuple<int,int>> corners1 = [];
int curX1= 1;
int curY1 = 1;

int cTrench2 = 0;
List<Tuple<int,int>> corners2 = [];
int curX2= 1;
int curY2 = 1;

while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        string[] split = s.Split(" ");
        string dir1 = split[0];
        int dist1 = int.Parse(split[1]);
        string color = split[2];
        int dist2 = int.Parse(color[2..7],System.Globalization.NumberStyles.HexNumber);
        string dir2 = color[7..8];
        if(dir1=="U"){
            cTrench1 += dist1;
            curY1 = curY1-dist1;
            corners1.Add(new(curX1,curY1));
        }else if(dir1 == "D"){
            cTrench1 += dist1;
            curY1 = curY1+dist1;
            corners1.Add(new(curX1,curY1));
        }else if(dir1 == "L"){
            cTrench1 += dist1;
            curX1 = curX1-dist1;
            corners1.Add(new(curX1,curY1));
        }else if(dir1 == "R"){
            cTrench1 += dist1;
            curX1 = curX1+dist1;
            corners1.Add(new(curX1,curY1));
        }else{
            throw new("nope");
        }

        if(dir2=="3"){
            cTrench2 += dist2;
            curY2 = curY2-dist2;
            corners2.Add(new(curX2,curY2));
        }else if(dir2=="1"){
            cTrench2 += dist2;
            curY2 = curY2+dist2;
            corners2.Add(new(curX2,curY2));
        }else if(dir2=="2"){
            cTrench2 += dist2;
            curX2 = curX2-dist2;
            corners2.Add(new(curX2,curY2));
        }else if(dir2=="0"){
            cTrench2 += dist2;
            curX2 = curX2+dist2;
            corners2.Add(new(curX2,curY2));
        }else{
            throw new("nope");
        }
    }
}
corners1.Add(corners1.First());
corners2.Add(corners2.First());

Console.WriteLine("part 1: " + interior(corners1, cTrench1));
Console.WriteLine("part 2: " + interior(corners2, cTrench2));

static long interior(List<Tuple<int,int>> corners, long cTrench){
    BigInteger sum = BigInteger.Zero;
    for(int i = 0; i < corners.Count-1; i++){
        BigInteger b1 = BigInteger.Add(corners[i].Item2, corners[i+1].Item2);
        BigInteger b2 = BigInteger.Subtract(corners[i].Item1, corners[i+1].Item1);
        BigInteger b3 = BigInteger.Multiply(b1,b2);
        sum = BigInteger.Add(sum, b3);
    }
    sum = BigInteger.Abs(sum);
    sum = BigInteger.Divide(sum, 2);
    long pick = ((long)sum) - cTrench/2 + 1;
    return pick + cTrench;
}