using System.Collections.Immutable;

StreamReader reader = new(File.OpenRead("./day17/input.txt")); 

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

HashSet<Tuple<Tuple<int,int>,Tuple<char,int>>> done = [];
PriorityQueue<ImmutableList<Tuple<Tuple<int,int>,char>>, int> front = new();
front.Enqueue([new Tuple<Tuple<int,int>,char>(new(0,0),'-')], 0);
ImmutableList<Tuple<Tuple<int, int>, char>>? path = null;

while(front.Count != 0){
    ImmutableList<Tuple<Tuple<int, int>, char>> list = front.Dequeue();
    Tuple<Tuple<int,int>,char> last = list.Last();
    Tuple<int,int>  coords = last.Item1;
    Tuple<char,int> count = Count(list);
    if(done.Contains(new(coords, count))){
        continue;
    }
    if(coords.Equals(new Tuple<int,int>(mX-1, mY-1))){
        path = list;
        break;
    }
    done.Add(new(coords, count));


    if(last.Item2 == '-') {
        if(coords.Item1-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1-1,coords.Item2),'<'));
            front.Enqueue(r1, Heat(map,r1));
        }
        if(coords.Item1+1 < mX){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1+1,coords.Item2),'>'));
            front.Enqueue(r2, Heat(map,r2));
        }
        if(coords.Item2-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1,coords.Item2-1),'^'));
            front.Enqueue(r3, Heat(map,r3));
        }
        if(coords.Item2+1 < mY){
            ImmutableList<Tuple<Tuple<int, int>, char>> r4 = list.Add(new(new(coords.Item1,coords.Item2+1),'v'));
            front.Enqueue(r4, Heat(map,r4));
        }
    }else if(last.Item2 == '>') {
        if(coords.Item2-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1,coords.Item2-1),'^'));
            front.Enqueue(r1, Heat(map,r1));
        }
        if(coords.Item2+1 < mY){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1,coords.Item2+1),'v'));
            front.Enqueue(r2, Heat(map,r2));
        }
        if(!(count.Item2==3) && coords.Item1+1 < mX){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1+1,coords.Item2),'>'));
            front.Enqueue(r3, Heat(map,r3));
        }
    }else if(last.Item2 == '<') {
        if(coords.Item2-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1,coords.Item2-1),'^'));
            front.Enqueue(r1, Heat(map,r1));
        }
        if(coords.Item2+1 < mY){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1,coords.Item2+1),'v'));
            front.Enqueue(r2, Heat(map,r2));
        }
        if(!(count.Item2==3) && coords.Item1-1 >=0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1-1,coords.Item2),'<'));
            front.Enqueue(r3, Heat(map,r3));
        }
    }else if(last.Item2 == '^') {
        if(coords.Item1-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1-1,coords.Item2),'<'));
            front.Enqueue(r1, Heat(map,r1));
        }
        if(coords.Item1+1 < mX){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1+1,coords.Item2),'>'));
            front.Enqueue(r2, Heat(map,r2));
        }
        if(!(count.Item2==3) && coords.Item2-1 >=0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1,coords.Item2-1),'^'));
            front.Enqueue(r3, Heat(map,r3));
        }
    }else if(last.Item2 == 'v') {
        if(coords.Item1-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1-1,coords.Item2),'<'));
            front.Enqueue(r1, Heat(map,r1));
        }
        if(coords.Item1+1 < mX){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1+1,coords.Item2),'>'));
            front.Enqueue(r2, Heat(map,r2));
        }
        if(!(count.Item2==3) && coords.Item2+1 <mY){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1,coords.Item2+1),'v'));
            front.Enqueue(r3, Heat(map,r3));
        }
    }
}
if(path == null){
    Environment.Exit(-1);
}
Console.WriteLine("part1: " + Heat(map,path));

HashSet<Tuple<Tuple<int,int>,Tuple<char,int>>> done2 = [];
PriorityQueue<ImmutableList<Tuple<Tuple<int,int>,char>>, int> front2 = new();
front2.Enqueue([new Tuple<Tuple<int,int>,char>(new(0,0),'-')], 0);
ImmutableList<Tuple<Tuple<int, int>, char>>? path2 = null;

while(front2.Count != 0){
    ImmutableList<Tuple<Tuple<int, int>, char>> list = front2.Dequeue();
    Tuple<Tuple<int,int>,char> last = list.Last();
    Tuple<int,int>  coords = last.Item1;
    Tuple<char,int> count = Count(list);
    if(done2.Contains(new(coords, count))){
        continue;
    }
    if(coords.Equals(new Tuple<int,int>(mX-1, mY-1))){
        if(count.Item2>=4){
            path2 = list;
            break;
        }else{
            continue;
        }
    }
    done2.Add(new(coords, count));


    if(last.Item2 == '-') {
        if(coords.Item1-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1-1,coords.Item2),'<'));
            front2.Enqueue(r1, Heat(map,r1));
        }
        if(coords.Item1+1 < mX){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1+1,coords.Item2),'>'));
            front2.Enqueue(r2, Heat(map,r2));
        }
        if(coords.Item2-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1,coords.Item2-1),'^'));
            front2.Enqueue(r3, Heat(map,r3));
        }
        if(coords.Item2+1 < mY){
            ImmutableList<Tuple<Tuple<int, int>, char>> r4 = list.Add(new(new(coords.Item1,coords.Item2+1),'v'));
            front2.Enqueue(r4, Heat(map,r4));
        }
    }else if(last.Item2 == '>') {
        if(count.Item2>=4 && coords.Item2-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1,coords.Item2-1),'^'));
            front2.Enqueue(r1, Heat(map,r1));
        }
        if(count.Item2>=4 && coords.Item2+1 < mY){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1,coords.Item2+1),'v'));
            front2.Enqueue(r2, Heat(map,r2));
        }
        if(count.Item2<10 && coords.Item1+1 < mX){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1+1,coords.Item2),'>'));
            front2.Enqueue(r3, Heat(map,r3));
        }
    }else if(last.Item2 == '<') {
        if(count.Item2>=4 && coords.Item2-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1,coords.Item2-1),'^'));
            front2.Enqueue(r1, Heat(map,r1));
        }
        if(count.Item2>=4 && coords.Item2+1 < mY){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1,coords.Item2+1),'v'));
            front2.Enqueue(r2, Heat(map,r2));
        }
        if(count.Item2<10 && coords.Item1-1 >=0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1-1,coords.Item2),'<'));
            front2.Enqueue(r3, Heat(map,r3));
        }
    }else if(last.Item2 == '^') {
        if(count.Item2>=4 && coords.Item1-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1-1,coords.Item2),'<'));
            front2.Enqueue(r1, Heat(map,r1));
        }
        if(count.Item2>=4 && coords.Item1+1 < mX){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1+1,coords.Item2),'>'));
            front2.Enqueue(r2, Heat(map,r2));
        }
        if(count.Item2<10 && coords.Item2-1 >=0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1,coords.Item2-1),'^'));
            front2.Enqueue(r3, Heat(map,r3));
        }
    }else if(last.Item2 == 'v') {
        if(count.Item2>=4 && coords.Item1-1 >= 0){
            ImmutableList<Tuple<Tuple<int, int>, char>> r1 = list.Add(new(new(coords.Item1-1,coords.Item2),'<'));
            front2.Enqueue(r1, Heat(map,r1));
        }
        if(count.Item2>=4 && coords.Item1+1 < mX){
            ImmutableList<Tuple<Tuple<int, int>, char>> r2 = list.Add(new(new(coords.Item1+1,coords.Item2),'>'));
            front2.Enqueue(r2, Heat(map,r2));
        }
        if(count.Item2<10 && coords.Item2+1 <mY){
            ImmutableList<Tuple<Tuple<int, int>, char>> r3 = list.Add(new(new(coords.Item1,coords.Item2+1),'v'));
            front2.Enqueue(r3, Heat(map,r3));
        }
    }
}
if(path2 == null){
    Environment.Exit(-1);
}
Console.WriteLine("part2: " + Heat(map,path2));




Dictionary<Tuple<int, int>, char> d = [];
foreach(Tuple<Tuple<int, int>, char> t in path2){
    d.Add(t.Item1, t.Item2);
}

IEnumerable<Tuple<int, int>> en = path2.Select(x => x.Item1);

for (int y = 0; y < mY; y++){
    for(int x = 0; x < mX; x++){
        if(en.Contains(new(x,y))){
            Console.Write(d[new(x,y)]);
        }else{
            Console.Write(map[y][x]);
        }
    }
    Console.WriteLine();
}

static int Heat(List<String> map, ImmutableList<Tuple<Tuple<int, int>, char>> list){
    int sum = 0;
    for (int i = 1; i < list.Count; i++){
        Tuple<int, int> pos = list[i].Item1;
        sum += int.Parse(""+map[pos.Item2][pos.Item1]);
    }
    return sum;
}

static Tuple<char,int> Count(ImmutableList<Tuple<Tuple<int, int>, char>> list){
    char c = list.Last().Item2;
    int i = list.Count-1;
    int count = 0;
    while(i>=0 && list[i].Item2==c){
        i--;
        count++;
    }
    return new(c, count);
}