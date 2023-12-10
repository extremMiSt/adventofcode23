StreamReader reader = new(File.OpenRead("./day10/input.txt")); 

char[,] map = new char[140,140]; //input
//char[,] map = new char[5,5]; //example1
//char[,] map = new char[20,10]; //example2
//char[,] map = new char[10,9]; //example3

int line = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        for (int i = 0; i < s.Length; i++){
            map[i, line] = s[i];
        }
    }
    line++;
}

int x = 0;
int y = 0;
bool found = false;
for (; x < map.GetLength(0); x++){
    for (; y < map.GetLength(1); y++){
        if(map[x,y] == 'S') {
            found = true;
            break;
        }
    }
    if(found){
        break;
    }else{
        y=0;
    }
}

Tuple<int,int> start = new(x,y);
List<Tuple<int,int>> candidates = [];
if(x-1>=0 && (map[x-1,y]=='F' || 
                map[x-1,y]=='-' ||
                map[x-1,y]=='L')){
    candidates.Add(new(x-1,y));
}else if(x+1<map.GetLength(0) && (map[x+1,y]=='7' || 
                map[x+1,y]=='-' ||
                map[x+1,y]=='J')){
    candidates.Add(new(x+1,y));
}else if(y-1>=0 && (map[x,y-1]=='|' || 
                map[x,y-1]=='7' ||
                map[x,y-1]=='F')){
    candidates.Add(new(x,y-1));
}else if(y+1<map.GetLength(1) && (map[x,y+1]=='|' || 
                map[x,y+1]=='L' ||
                map[x,y+1]=='J')){
    candidates.Add(new(x,y+1));
}

Tuple<int,int> n1 = candidates[0];
Tuple<int,int>[] ns = next(n1,map);
Tuple<int,int> n2 = ns[0].Equals(start)? ns[1]: ns[0];

List<Tuple<int,int>> path = [n1,n2];
while(!path.Last().Equals(start)){
    Tuple<int,int> c = path.Last();
    Tuple<int,int> p = path[^2];
    Tuple<int,int>[] nexts = next(c, map);
    path.Add(nexts[0].Equals(p)? nexts[1]: nexts[0]);
}
Console.WriteLine("task 1: " + path.Count/2);

HashSet<Tuple<int, int>> pathSet = [];
foreach(Tuple<int,int> el in path){
    int x1 = el.Item1*2+1;
    int y1 = el.Item2*2+1;
    pathSet.Add(new(x1,y1));
    if(map[el.Item1,el.Item2] == '|'){
        pathSet.Add(new(x1, y1-1));
        pathSet.Add(new(x1,y1+1));
    }else if(map[el.Item1,el.Item2] == '-'){
        pathSet.Add(new(x1-1,y1));
        pathSet.Add(new(x1+1,y1));
    }else if(map[el.Item1,el.Item2] == 'L'){
        pathSet.Add(new(x1,y1-1));
        pathSet.Add(new(x1+1,y1));
    }else if(map[el.Item1,el.Item2] == 'J'){
        pathSet.Add(new(x1,y1-1));
        pathSet.Add(new(x1-1,y1));
    }else if(map[el.Item1,el.Item2] == '7'){
        pathSet.Add(new(x1,y1+1));
        pathSet.Add(new(x1-1,y1));
    }else if(map[el.Item1,el.Item2] == 'F'){
        pathSet.Add(new(x1,y1+1));
        pathSet.Add(new(x1+1,y1));
    }
}

HashSet<Tuple<int,int>> outside = [new(0,0)];
Queue<Tuple<int,int>> front = [];
front.Enqueue(new(0,0));
while(front.Count>0){
    Tuple<int,int> cur = front.Dequeue();
    if(cur.Item1-1 >=0 && !pathSet.Contains(new(cur.Item1-1, cur.Item2)) && !outside.Contains(new(cur.Item1-1, cur.Item2))){
        outside.Add(new(cur.Item1-1, cur.Item2));
        front.Enqueue(new(cur.Item1-1, cur.Item2));
    }
    if(cur.Item1+1 <= map.GetLength(0)*2 && !pathSet.Contains(new(cur.Item1+1, cur.Item2)) && !outside.Contains(new(cur.Item1+1, cur.Item2))){
        outside.Add(new(cur.Item1+1, cur.Item2));
        front.Enqueue(new(cur.Item1+1, cur.Item2));
    }
    if(cur.Item2-1 >=0 && !pathSet.Contains(new(cur.Item1, cur.Item2-1)) && !outside.Contains(new(cur.Item1, cur.Item2-1))){
        outside.Add(new(cur.Item1, cur.Item2-1));
        front.Enqueue(new(cur.Item1, cur.Item2-1));
    }
    if(cur.Item2+1 <= map.GetLength(1)*2 && !pathSet.Contains(new(cur.Item1, cur.Item2+1)) && !outside.Contains(new(cur.Item1, cur.Item2+1))){
        outside.Add(new(cur.Item1, cur.Item2+1));
        front.Enqueue(new(cur.Item1, cur.Item2+1));
    }
}

HashSet<Tuple<int,int>> filtered = [];
foreach (Tuple<int,int> item in outside){
    if(item.Item1%2==1 && item.Item2%2==1){
        filtered.Add(new((item.Item1-1)/2, (item.Item2-1)/2));
    }
}

Console.WriteLine("task 2: " + (map.GetLength(0)*map.GetLength(1) - path.Count - filtered.Count));

static Tuple<int,int>[] next(Tuple<int,int> c, char[,] map){
    if(map[c.Item1,c.Item2] == '|'){
        Tuple<int,int> prev = new(c.Item1,c.Item2-1);
        Tuple<int,int> next = new(c.Item1,c.Item2+1);
        return [prev,next];
    }else if(map[c.Item1,c.Item2] == '-'){
        Tuple<int,int> prev = new(c.Item1-1,c.Item2);
        Tuple<int,int> next = new(c.Item1+1,c.Item2);
        return [prev,next];
    }else if(map[c.Item1,c.Item2] == 'L'){
        Tuple<int,int> prev = new(c.Item1,c.Item2-1);
        Tuple<int,int> next = new(c.Item1+1,c.Item2);
        return [prev,next];
    }else if(map[c.Item1,c.Item2] == 'J'){
        Tuple<int,int> prev = new(c.Item1,c.Item2-1);
        Tuple<int,int> next = new(c.Item1-1,c.Item2);
        return [prev,next];
    }else if(map[c.Item1,c.Item2] == '7'){
        Tuple<int,int> prev = new(c.Item1,c.Item2+1);
        Tuple<int,int> next = new(c.Item1-1,c.Item2);
        return [prev,next];
    }else if(map[c.Item1,c.Item2] == 'F'){
        Tuple<int,int> prev = new(c.Item1,c.Item2+1);
        Tuple<int,int> next = new(c.Item1+1,c.Item2);
        return [prev,next];
    }
    throw new("oops");
}


