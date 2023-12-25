using DictionaryHelper;

StreamReader reader = new(File.OpenRead("./day25/input.txt")); 

Dictionary<string,List<string>> graph = [];

while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        string[] split = s.Split(": ");
        string key = split[0];
        List<string> adjacent = new(split[1].Split(" "));
        graph[key] = adjacent;
    }
}

Dictionary<string,List<string>> small = graph;
int size = count(graph);

while(size != 3){
    Dictionary<string,List<string>> cur = graph;
    while(cur.Keys.Count > 2){
        cur = contract(cur);
    }
    int newsize = count(cur);
    if(newsize < size){
        size = newsize;
        small = cur;
        Console.WriteLine(size);
    }
}

long res = 1;
foreach(string key in small.Keys){
    res *= key.Count()/3;
}
Console.WriteLine(res);


static Dictionary<string,List<string>> contract(Dictionary<string,List<string>> inp){
    int count = 0;
    foreach(string key in inp.Keys){
        count += inp[key].Count;
    }
    Random random = new Random(); 
    int r = random.Next(count);

    Tuple<string,string> rand  = new("!","!");
    foreach(string key in inp.Keys){
        if(r < inp[key].Count){
            rand = new(key, inp[key][r]);
        }else{
            r -= inp[key].Count;
        }
    }

    Dictionary<string,List<string>> next = [];
    string contracted = rand.Item1+rand.Item2;
    foreach(string key in inp.Keys){
        foreach(string el in inp[key]){
            if((key == rand.Item1 && el == rand.Item2) || (el == rand.Item1 && key == rand.Item2)){
                continue;
            }else if(key == rand.Item1 || key == rand.Item2){
                next.AddCreate(contracted, el);
            }else if(el == rand.Item1 || el == rand.Item2){
                next.AddCreate(key, contracted);
            }else{
                next.AddCreate(key, el);
            }
        }
    }
    return next;
}

static int count(Dictionary<string,List<string>> graph){
    int count = 0;
    foreach(string key in graph.Keys){
        count += graph[key].Count;
    }
    return count;
}