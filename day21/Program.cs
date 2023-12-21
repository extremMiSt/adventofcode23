StreamReader reader = new(File.OpenRead("./day21/example1.txt")); 
int steps = 6;


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

HashSet<Tuple<int,int>> reached = [];
Stack<Triple<int,int,int>> stack = [];
HashSet<Tuple<int,int>> done = []; 
stack.Push(new(xS,yS, 0));
while(stack.Count != 0){
    Triple<int,int,int> cur = stack.Pop();
    if(cur.C<=steps /*&& cur.C%2 == 0*/){
        reached.Add(new(cur.A,cur.B));
    }

    if(cur.A-1 >=0 && map[cur.B][cur.A-1]!= '#' && !done.Contains(new(cur.A, cur.B))/* && cur.C <= steps*/){
        stack.Push(new(cur.A-1, cur.B, cur.C+1));
    }
    if(cur.A+1 < map[cur.B].Length && map[cur.B][cur.A+1]!= '#' && !done.Contains(new(cur.A, cur.B))/* && cur.C <= steps*/){
        stack.Push(new(cur.A+1, cur.B, cur.C+1));
    }
    if(cur.B-1 >=0 && map[cur.B-1][cur.A]!= '#' && !done.Contains(new(cur.A, cur.B))/* && cur.C <= steps*/){
        stack.Push(new(cur.A, cur.B-1, cur.C+1));
    }
    if(cur.B+1 < map.Count && map[cur.B+1][cur.A]!= '#' && !done.Contains(new(cur.A, cur.B))/* && cur.C <= steps*/){
        stack.Push(new(cur.A, cur.B+1, cur.C+1));
    }
    done.Add(new(cur.A, cur.B));
}
Console.WriteLine(reached.Count);