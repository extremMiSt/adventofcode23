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
Dictionary<int, List<int>> supported = [];
Dictionary<int, List<int>> supports = [];
foreach(Brick b in bricks){
    if(b.Lowest==1){
        fallen.Add(b);
    }else{
        Brick fall = b;
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
    }
}


int count = 0;
foreach(Brick b in fallen){
    if(!supports.ContainsKey(b.Num)){
        count++;
        continue;
    }else{
        List<int> above = supports[b.Num];
        List<int> would = [];
        foreach(int i in above){
            if(supported[i].Count <2){
                would.Add(i);
            }
        }
        if(would.Count == 0){
            count++;
        }
    }
}
Console.WriteLine(count);


int max = 0;
fallen.Sort();
IEnumerable<Brick> rev = fallen.Reverse<Brick>();
foreach(Brick r in rev){
    int f = falling(r, supported, supports);
    if(f> max) max = f;
}
Console.WriteLine(max);
//158 is too low


static int falling(Brick disintegrated, Dictionary<int, List<int>> supported, Dictionary<int, List<int>> supports){
    if(!supports.ContainsKey(disintegrated.Num)){
        G.fell[disintegrated.Num] = [];
        return 0;
    }else{
        HashSet<int> s = [];
        List<int> above = wouldFall(disintegrated, supported, supports);
        foreach(int i in above){
            s.UnionWith(G.fell[i]);
            s.Add(i);
        }
        G.fell[disintegrated.Num]=s;
        return s.Count;
    }
}

static List<int> wouldFall(Brick disintegrated, HashSet<int> falling, Dictionary<int, List<int>> supported, Dictionary<int, List<int>> supports){
    List<int> above = supports[disintegrated.Num];
    List<int> would = [];
    foreach(int i in above){
        HashSet<int> sup = supported[i].RemoveAll(X => falling.Contains(X));
        if(supported[i].Count <2){
            would.Add(i);
        }
    }
    return would;
}

static void AddCreate<K,V>(Dictionary<K,List<V>> dict, K key, V elem) where K : notnull{
    if(dict.ContainsKey(key)){
        dict[key].Add(elem);
    }else{
        dict[key] = [elem];
    }
}



static int Count<K,V>(Dictionary<K,List<V>> dict, K key) where K : notnull{
    if(dict.ContainsKey(key)){
        return dict[key].Count;
    }else{
        return 0;
    }
}

static class G{
    static public Dictionary<int, HashSet<int>> fell = [];
}