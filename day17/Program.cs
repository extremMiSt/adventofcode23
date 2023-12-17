StreamReader reader = new(File.OpenRead("./day17/example1.txt")); 

List<String> map = [];

int mX = 0;
int mY = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        mX = s.Length;
        map.Add(s);
    }
    mY++;
}

List<Tuple<int,int>> done = [];
PriorityQueue<Tuple<List<Tuple<int,int>>,char>, int> front = new();
front.Enqueue(new([new(0,0)],'-'), 0);
