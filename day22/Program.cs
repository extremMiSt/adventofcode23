StreamReader reader = new(File.OpenRead("./day22/input.txt")); 

List<Brick> bricks = [];

int line = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        bricks.Add(new(s, line));
    }
    line++;
}
bricks.Sort();

List<Brick> fallen = [];
Dictionary<int, HashSet<int>> supported = [];
Dictionary<int, HashSet<int>> supports = [];
int height = 5;
foreach(Brick b in bricks){
    if(b.Lowest==1){
        fallen.Add(b);
    }else{
        Brick fall = b.LowerTo(Math.Min(b.Lowest, height+1));
        while(Count(supported,fall.Num)==0){
            if(fall.Lowest==1){
                break;
            }
            Brick lower = fall.Lower();
            foreach(Brick f in fallen){
                if(lower.Intersects(f)){
                    AddCreate(supported, fall.Num, f.Num);
                    AddCreate(supports, f.Num, fall.Num);
                }
            }
            if(Count(supported,fall.Num) != 0){
                break;
            }else{
            }
            fall = lower;
        }
        fallen.Add(fall);
        if(fall.Highest > height) height = fall.Highest;
    }
}


int count = 0;
HashSet<int> structural = [];
foreach(Brick b in fallen){
    if(!supports.ContainsKey(b.Num)){
        count++;
        continue;
    }else{
        HashSet<int> above = supports[b.Num];
        List<int> would = [];
        foreach(int i in above){
            if(supported[i].Count <2){
                would.Add(i);
            }
        }
        if(would.Count == 0){
            count++;
        }else{
            structural.Add(b.Num);
        }
    }
}
Console.WriteLine("part 1: " + count);


int max = 0;
fallen.Sort();
foreach(int cur in structural){

    HashSet<int> affected = [];
    Queue<int> q = [];
    q.Enqueue(cur);
    while(q.Count != 0){
        int head = q.Dequeue();
        affected.Add(head);

        if(!supports.ContainsKey(head)) continue;

        HashSet<int> above = supports[head];
        foreach(int i in above){
            HashSet<int> sup = new(supported[i]);
            sup.ExceptWith(affected);
            if(sup.Count == 0 && !affected.Contains(i)){
                q.Enqueue(i);
                affected.Add(i);
            }
        }
    }

    max += affected.Count - 1;
}
Console.WriteLine("part 2: " + max);


static void AddCreate<K,V>(Dictionary<K,HashSet<V>> dict, K key, V elem) where K : notnull{
    if(dict.ContainsKey(key)){
        dict[key].Add(elem);
    }else{
        dict[key] = [elem];
    }
}



static int Count<K,V>(Dictionary<K,HashSet<V>> dict, K key) where K : notnull{
    if(dict.ContainsKey(key)){
        return dict[key].Count;
    }else{
        return 0;
    }
}