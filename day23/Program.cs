using System.Collections.Immutable;
using Point = System.Tuple<int, int>;

StreamReader reader = new(File.OpenRead("./day23/input.txt")); 

List<string> map = [];

int line = 0;
int width = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        map.Add(s);
        width = s.Length;
    }
    line++;
}

Stack<Tuple<ImmutableHashSet<Point>,Point>> q = new();
ImmutableHashSet<Point> start = ImmutableHashSet.Create<Point>([new(1,0)]);
q.Push(new(start, new(1,0)));
List<ImmutableList<Point>> found = [];
int max = 0;
while(q.Count != 0){
    Tuple<ImmutableHashSet<Point>,Point> curs = q.Pop();
    ImmutableHashSet<Point> cur = curs.Item1;
    Point last = curs.Item2;
    if(last.Equals(new Point(width-2, line-1))){
        if(max < cur.Count){
            Console.WriteLine("found " + (cur.Count-1));
            max = cur.Count;
        }
        continue;
    }

    if(map[last.Item2][last.Item1] == '.'){
        if(last.Item1 - 1 >= 0 && map[last.Item2][last.Item1-1] != '#' && !cur.Contains(new(last.Item1-1,last.Item2))){
            q.Push(new(cur.Add(new(last.Item1-1,last.Item2)),new(last.Item1-1,last.Item2)));
        }
        if(last.Item1 + 1 < width && map[last.Item2][last.Item1+1] != '#' && !cur.Contains(new(last.Item1+1,last.Item2))){
            q.Push(new(cur.Add(new(last.Item1+1,last.Item2)),new(last.Item1+1,last.Item2)));
        }
        if(last.Item2 - 1 >= 0 && map[last.Item2-1][last.Item1] != '#' && !cur.Contains(new(last.Item1,last.Item2-1))){
            q.Push(new(cur.Add(new(last.Item1,last.Item2-1)),new(last.Item1,last.Item2-1)));
        }
        if(last.Item2 + 1 < line && map[last.Item2+1][last.Item1] != '#' && !cur.Contains(new(last.Item1,last.Item2+1))){
            q.Push(new(cur.Add(new(last.Item1,last.Item2+1)),new(last.Item1,last.Item2+1)));
        }
    }else if(map[last.Item2][last.Item1] == '<' && last.Item1 - 1 >= 0 && map[last.Item2][last.Item1-1] != '#' && !cur.Contains(new(last.Item1-1,last.Item2))){
        q.Push(new(cur.Add(new(last.Item1-1,last.Item2)),new(last.Item1-1,last.Item2)));
    }else if(map[last.Item2][last.Item1] == '>' && last.Item1 + 1 < width && map[last.Item2][last.Item1+1] != '#' && !cur.Contains(new(last.Item1+1,last.Item2))){
        q.Push(new(cur.Add(new(last.Item1+1,last.Item2)),new(last.Item1+1,last.Item2)));
    }else if(map[last.Item2][last.Item1] == '^' && last.Item2 - 1 >= 0 && map[last.Item2-1][last.Item1] != '#' && !cur.Contains(new(last.Item1,last.Item2-1))){
        q.Push(new(cur.Add(new(last.Item1,last.Item2-1)),new(last.Item1,last.Item2-1)));
    }else if(map[last.Item2][last.Item1] == 'v' && last.Item2 + 1 < line && map[last.Item2+1][last.Item1] != '#' && !cur.Contains(new(last.Item1,last.Item2+1))){
        q.Push(new(cur.Add(new(last.Item1,last.Item2+1)),new(last.Item1,last.Item2+1)));
    }else{
        //throw new("nope: " + map[last.Item2][last.Item1] + " " + last);
    }
}
Console.WriteLine("task1: " + (max-1));

Stack<Tuple<ImmutableHashSet<Point>,Point>> q2 = new();
ImmutableHashSet<Point> start2 = ImmutableHashSet.Create<Point>([new(1,0)]);
q2.Push(new(start2, new(1,0)));

int max2 = 0;
while(q2.Count != 0){
    Tuple<ImmutableHashSet<Point>,Point> curs = q2.Pop();
    ImmutableHashSet<Point> cur = curs.Item1;
    Point last = curs.Item2;
    if(last.Equals(new Point(width-2, line-1))){
        if(max2 < cur.Count){
            Console.WriteLine("found " + (cur.Count-1));
            max2 = cur.Count;
        }
        continue;
    }

    if(map[last.Item2][last.Item1] != '#'){
        if(last.Item1 - 1 >= 0 && map[last.Item2][last.Item1-1] != '#' && !cur.Contains(new(last.Item1-1,last.Item2))){
            q2.Push(new(cur.Add(new(last.Item1-1,last.Item2)), new(last.Item1-1,last.Item2)));
        }
        if(last.Item1 + 1 < width && map[last.Item2][last.Item1+1] != '#' && !cur.Contains(new(last.Item1+1,last.Item2))){
            q2.Push(new(cur.Add(new(last.Item1+1,last.Item2)),new(last.Item1+1,last.Item2)));
        }
        if(last.Item2 - 1 >= 0 && map[last.Item2-1][last.Item1] != '#' && !cur.Contains(new(last.Item1,last.Item2-1))){
            q2.Push(new(cur.Add(new(last.Item1,last.Item2-1)),new(last.Item1,last.Item2-1)));
        }
        if(last.Item2 + 1 < line && map[last.Item2+1][last.Item1] != '#' && !cur.Contains(new(last.Item1,last.Item2+1))){
            q2.Push(new(cur.Add(new(last.Item1,last.Item2+1)),new(last.Item1,last.Item2+1)));
        }
    }
}

Console.WriteLine("task2: " + (max2-1));