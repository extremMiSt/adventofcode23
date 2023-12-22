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


static int falling(Brick disintegrated, Dictionary<int, List<int>> supported, Dictionary<int, List<int>> supports){
    if(!supports.ContainsKey(disintegrated.Num)){
        G.fell[disintegrated.Num] = 0;
        return 0;
    }else{
        int s = 0;
        List<int> above = supports[disintegrated.Num];
        foreach(int i in above){
            
        }
    }
    return 0;
}

static bool wouldFall(Brick disintegrated, Dictionary<int, List<int>> supported, Dictionary<int, List<int>> supports){
    List<int> above = supports[disintegrated.Num];
    List<int> would = [];
    foreach(int i in above){
        if(supported[i].Count <2){
            would.Add(i);
        }
    }
    return would.Count == 0;
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
    static public Dictionary<int, int> fell = [];
}