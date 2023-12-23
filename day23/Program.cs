using System.Collections.Immutable;
using Point = System.Tuple<int, int>;

StreamReader reader = new(File.OpenRead("./day23/example1.txt")); 

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

Stack<ImmutableList<Point>> q = new();
ImmutableList<Point> start = ImmutableList.Create<Point>([new(0,1)]);
q.Push(start);

while(q.Count != 0){
    
}