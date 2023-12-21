using System.Numerics;

StreamReader reader = new(File.OpenRead("./day21/input.txt")); 
int steps = 64;


List<String> map = [];
int xS=-1;
int yS=-1;

int line = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        map.Add(s);
        if(s.Contains('S')){
            yS = line;
            xS = s.IndexOf('S');
        }
    }
    line++;
}

Console.WriteLine(xS + " " + yS);
Console.WriteLine("part1: " + reacheable(xS, yS, steps, map));
Console.WriteLine("data: " + reacheableRec(xS, yS, 4, map));
Console.WriteLine("data: " + reacheableRec(xS, yS, 10, map));
Console.WriteLine("data: " + reacheableRec(xS, yS, 50, map));
double inp = 26501365;
double a = 15387;
double den = 17161;
double b = 28618;
double c = 246426;

double res = (a*inp*inp)/den + (b*inp)/den - c/den;
Console.WriteLine(res);
//6380079850264089 too high
//629720570456281
//629720570456282
//600623511205421 too low
//667128043313758 is wrong too...
//592316293533198


static long reacheable(int xS, int yS, int steps, List<String> map){
    HashSet<Tuple<int,int>> reached = [];
    Queue<Triple<int,int,int>> q = [];
    HashSet<Tuple<int,int>> done = []; 
    q.Enqueue(new(xS,yS, 0));
    while(q.Count != 0){
        Triple<int,int,int> cur = q.Dequeue();
        if(cur.C<=steps && cur.C%2 == 0){
            reached.Add(new(cur.A,cur.B));
        }

        if(cur.A-1 >=0 && map[cur.B][cur.A-1]!= '#' && !done.Contains(new(cur.A, cur.B))&& cur.C <= steps){
            q.Enqueue(new(cur.A-1, cur.B, cur.C+1));
        }
        if(cur.A+1 < map[cur.B].Length && map[cur.B][cur.A+1]!= '#' && !done.Contains(new(cur.A, cur.B))&& cur.C <= steps){
            q.Enqueue(new(cur.A+1, cur.B, cur.C+1));
        }
        if(cur.B-1 >=0 && map[cur.B-1][cur.A]!= '#' && !done.Contains(new(cur.A, cur.B)) && cur.C <= steps){
            q.Enqueue(new(cur.A, cur.B-1, cur.C+1));
        }
        if(cur.B+1 < map.Count && map[cur.B+1][cur.A]!= '#' && !done.Contains(new(cur.A, cur.B))&& cur.C <= steps){
            q.Enqueue(new(cur.A, cur.B+1, cur.C+1));
        }
        done.Add(new(cur.A, cur.B));
    }
    return reached.Count;
}

static long reacheableRec(int xS, int yS, int steps, List<String> map){
    HashSet<Tuple<int,int>> reached = [];
    Queue<Triple<int,int,int>> q = [];
    HashSet<Tuple<int,int>> done = []; 
    q.Enqueue(new(xS,yS, 0));
    while(q.Count != 0){
        Triple<int,int,int> cur = q.Dequeue();
        if(cur.C<=steps && cur.C%2 == steps%2){
            reached.Add(new(cur.A,cur.B));
        }
        int d = map.Count;
        if(map[mod(cur.B,d)][mod(cur.A-1,d)]!= '#' && !done.Contains(new(cur.A, cur.B))&& cur.C <= steps){
            q.Enqueue(new(cur.A-1, cur.B, cur.C+1));
        }
        if(map[mod(cur.B,d)][mod(cur.A+1,d)]!= '#' && !done.Contains(new(cur.A, cur.B))&& cur.C <= steps){
            q.Enqueue(new(cur.A+1, cur.B, cur.C+1));
        }
        if(map[mod(cur.B-1,d)][mod(cur.A,d)]!= '#' && !done.Contains(new(cur.A, cur.B)) && cur.C <= steps){
            q.Enqueue(new(cur.A, cur.B-1, cur.C+1));
        }
        if(map[mod(cur.B+1,d)][mod(cur.A,d)]!= '#' && !done.Contains(new(cur.A, cur.B))&& cur.C <= steps){
            q.Enqueue(new(cur.A, cur.B+1, cur.C+1));
        }
        done.Add(new(cur.A, cur.B));
    }
    return reached.Count;
}


static int mod(int i, int d){
    int i2 = i%d;
    if(i2<0){
        i2+=d;
    }
    return i2;
}