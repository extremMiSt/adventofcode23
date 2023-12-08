StreamReader reader = new(File.OpenRead("./day08/input.txt")); 

string? instructions = reader.ReadLine();
if(instructions is null){
    System.Environment.Exit(0);  
}
reader.ReadLine();
Dictionary<string, string[]> map = [];

while(!reader.EndOfStream){
    string? s = reader.ReadLine();
    if(s is not null){
        string[] split = s.Split(" = (");
        string[] targets = split[1].Substring(0, split[1].Length-1).Split(", ");
        map[split[0]] = targets;
    }
}

int step = 0;
bool found = false;
string current = "AAA";
while(!found){
    foreach(char r in instructions){
        if(current == "ZZZ"){
            found = true;
            break;
        }
        if(r=='L'){
            current = map[current][0];
        }else if(r=='R'){
            current = map[current][1];
        }
        step++;
    }
}
Console.WriteLine(step);

List<string> aas = [];
foreach(string node in map.Keys){
    if(node.EndsWith('A')){
        aas.Add(node);
    }
}

long step2 = 0;
bool found2 = false;

while(!found2){
    foreach(char r in instructions){
        
        found2 = true;
        for(int i=0; i < aas.Count; i++){
            string node = aas[i];
            if(!node.EndsWith('Z')){
                found2 = false;
            }else{
                Console.WriteLine(i + " " + node + " " +step2);
            }
        }
        if(found2){
            break;
        }
        for (int i = 0; i < aas.Count; i++){
            if(r=='L'){
                aas[i] = map[aas[i]][0];
            }else if(r=='R'){
                aas[i] = map[aas[i]][1];
            }
        }
        step2++;
    }
}
/*
obervations: 
index 0 gets to LNZ at step 20569, then gets to the same node every 20569 steps
index 1 gets to HPZ at step 18727, then gets to the same node every 18727 steps
index 2 gets to ZZZ at step 14429, then gets to the same node every 14429 steps
index 3 gets to SGZ at step 13201, then gets to the same node every 13201 steps
index 4 gets to CXZ at step 18113, then gets to the same node every 18113 steps
index 5 gets to CFZ at step 22411, then gets to the same node every 22411 steps

LCM(20569, 18727, 14429, 13201, 18113, 22411) = 10921547990923
which is my solution.
*/
Console.WriteLine(step2);