StreamReader reader = new(File.OpenRead("./day20/input.txt")); 

Dictionary<string,Module> modules = [];
List<Conjuction> conjuctions = [];

while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        string[] split1 = s.Split(" -> ");
        string name = split1[0];
        string[] dest = split1[1].Split(", ");
        if(name.StartsWith('b')){
            modules.Add(name, new Broadcast(name, dest));
        }else if(s.StartsWith('%')){
            modules.Add(name[1..], new FlipFlop(name[1..], dest));
        }else if(s.StartsWith('&')){
            Conjuction c = new Conjuction(name[1..], dest);
            modules.Add(name[1..], c);
            conjuctions.Add(c);
        }else{
            throw new("nope");
        }
    }
}
foreach(Conjuction c in conjuctions){
    c.SetPredecessors(modules.Where(x => x.Value.Destinations.Contains(c.Name)).Select(x => x.Key));
}

long countHigh = 0;
long countLow = 0;
for(int i = 0; i < 1000; i++){
    Queue<Triple<string,string,bool>> q = new();
    q.Enqueue(new("button", "broadcaster", false));
    while(q.Count != 0){
        Triple<string,string,bool> cur = q.Dequeue();
        if(cur.C){
            countHigh++;
        }else{
            countLow++;
        }
        if(modules.ContainsKey(cur.B)){
            List<Triple<string, string, bool>> next = modules[cur.B].Pulse(cur.A, cur.C);
            next.ForEach(q.Enqueue);
        }
    }
}
Console.WriteLine("part1: " + countHigh*countLow);


string collector = modules.Where(m => m.Value.Destinations.Contains("rx")).Select(m => m.Key).First();
List<string> loops = new(modules.Where(m => m.Value.Destinations.Contains(collector)).Select(m => m.Key));

Dictionary<string, long[]> cycle = [];

foreach(string l in loops){

    foreach(string s in modules.Keys){
        modules[s].Reset();
    }

    long presses = 0;
    long last = 0;
    bool lastB = false;
    int found = 0;
    long[] res = new long[2];
    while(found < 2){
        presses += 1;
        Queue<Triple<string,string,bool>> q = new();
        q.Enqueue(new("button", "broadcaster", false));
        while(q.Count != 0){
            Triple<string,string,bool> cur = q.Dequeue();
            if(modules.ContainsKey(cur.B)){
                if(cur.B==collector && cur.A==l && (cur.C!=lastB)){
                    if(cur.C){
                        res[found] = presses-last;
                        last = presses;
                        found ++;
                    }
                    lastB = cur.C;
                    break;
                }
                List<Triple<string, string, bool>> next = modules[cur.B].Pulse(cur.A, cur.C);
                next.ForEach(q.Enqueue);
            }
        }
    }
    cycle[l] = res;
}

/*
okay imma be honest: 
I tried this out of desperation, cause someone suggested to me that this is what was asked for.
but as far as I understood the task, this is utterly wrong.

this assumes that the initial offset `cycle[k][0]` is the same as the cycle-length 
`cycle[k][1]`, which it very much isn't.

when I tried to take into account the differing cycle lengths and offsets I got into 
the situation that no solution existed. see calc.ods, where F10 not being an integer 
means that there is no solution.

but AoC accepted this as the correct answer...
*/
long solution = 1;
foreach(string k in cycle.Keys){
    solution = lcm(solution, cycle[k][0]);
}
Console.WriteLine("part2: " + solution);


static long gcd(long a, long b){
    while (b != 0){
        long temp = b;
        b = a % b;
        a = temp;
    }
    return a;
}

static long lcm(long a, long b){
    return (a / gcd(a, b)) * b;
}