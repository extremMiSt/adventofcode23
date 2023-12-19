using System.Runtime.Remoting;

StreamReader reader = new(File.OpenRead("./day19/input.txt")); 

Dictionary<string,List<Rule>> rules = [];

while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        if(s==""){
            break;
        }
        string[] split1 = s.Split('{');
        string rulename = split1[0];
        string[] ruleS = split1[1].Split(',');
        List<Rule> rs = [];
        foreach(string rule in ruleS){
            if(rule.Contains('<')){
                string[] split2 = rule.Split(':');
                string[] split3 = split2[0].Split('<');
                rs.Add(new(split3[0], int.Parse(split3[1]), true, split2[1], false));
            }else if(rule.Contains('>')){
                string[] split2 = rule.Split(':');
                string[] split3 = split2[0].Split('>');
                rs.Add(new(split3[0], int.Parse(split3[1]), false, split2[1], false));

            }else if(rule.EndsWith('}')){
                string[] split2 = rule.Split('}');
                rs.Add(new("", 0, false, split2[0], true));

            }else{
                throw new("nope");
            }
        }
        rules.Add(rulename, rs);
    }
}

long sum = 0;
while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        Dictionary<string,int> obj = [];
        string[] elems = s[1..^1].Split(',');
        foreach(string e in elems){
            string[] split = e.Split('=');
            obj.Add(split[0], int.Parse(split[1]));
        }
        string cur = "in";
        while(!(cur=="A" || cur =="R")){
            List<Rule> rs = rules[cur];
            foreach(Rule rule in rs){
                if(rule.Matches(obj)){
                    cur = rule.Next;
                    break;
                }
            }
        }
        if(cur=="A"){
            sum += obj["x"]+obj["m"]+obj["a"]+obj["s"];
        }
    }
}
Console.WriteLine(sum);

Dictionary<string, List<Dictionary<string, Tuple<long,long>>>> todo = [];
Dictionary<string, Tuple<long,long>> start = [];
start["x"] = new(1,4000);
start["m"] = new(1,4000);
start["a"] = new(1,4000);
start["s"] = new(1,4000);
todo.Add("in", [start]);

long sum2 = 0;
while(todo.Count != 0){
    KeyValuePair<string, List<Dictionary<string, Tuple<long, long>>>> first = todo.First();
    List<Dictionary<string, Tuple<long, long>>> list = first.Value;
    todo.Remove(first.Key);
    string key = first.Key;

    if(key == "A"){
        foreach(Dictionary<string, Tuple<long, long>> obj in list){
            sum2 += (obj["x"].Item2-obj["x"].Item1+1)*(obj["m"].Item2-obj["m"].Item1+1)*
                (obj["a"].Item2-obj["a"].Item1+1)*(obj["s"].Item2-obj["s"].Item1+1);
        }
        continue;
    }
    if(key == "R"){
        continue;
    }

    List<Rule> rs = rules[key];

    foreach(Dictionary<string, Tuple<long, long>> obj in list){
        Dictionary<string, Tuple<long, long>> cur = obj;
        foreach(Rule r in rs){
            if(r.Default){
                AddCreate(todo, r.Next, cur);
            }else{
                Dictionary<string, Tuple<long, long>> match = [];
                bool matchB = true;
                Dictionary<string, Tuple<long, long>> nomatch = [];
                bool noB = true;
                foreach(KeyValuePair<string,Tuple<long,long>> val in cur){
                    if(r.Key != val.Key){
                        match[val.Key] = val.Value;
                        nomatch[val.Key] = val.Value;
                    }else if(r.Less){
                        match[val.Key] = new(val.Value.Item1, Math.Min(val.Value.Item2, r.Param-1));
                        matchB = match[val.Key].Item1 <= match[val.Key].Item2;
                        nomatch[val.Key] = new(Math.Max(val.Value.Item1, r.Param), val.Value.Item2);
                        noB = nomatch[val.Key].Item1 <= nomatch[val.Key].Item2;
                    }else{
                        match[val.Key] = new(Math.Max(val.Value.Item1, r.Param)+1, val.Value.Item2);
                        matchB = match[val.Key].Item1 <= match[val.Key].Item2;
                        nomatch[val.Key] = new(val.Value.Item1, Math.Min(val.Value.Item2, r.Param));
                        noB = nomatch[val.Key].Item1 <= nomatch[val.Key].Item2;
                    }
                }
                if(matchB){
                    AddCreate(todo, r.Next, match);
                }
                if(!noB){
                    break;
                }
                cur = nomatch;
            }
        }
    }
}
Console.WriteLine(sum2);

static void AddCreate<K,V>(Dictionary<K,List<V>> dict, K key, V elem) where K : notnull{
    if(dict.ContainsKey(key)){
        dict[key].Add(elem);
    }else{
        dict[key] = [elem];
    }
}