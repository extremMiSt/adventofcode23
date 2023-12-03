using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

int size = 140;

StreamReader reader = new StreamReader(File.OpenRead("./day03/input.txt")); 

char[,] schematic = new char[size,size];
HashSet<char> symbols = ['*','@','/','+','$','=','&','-','#','%'];
HashSet<char> numbers = ['3','5','8','1','9','6','2','7','4','0'];

int line = 0;
while(reader.Peek() >= 0){
    string? s = reader.ReadLine();
    if(s is not null){
        for (int x = 0; x < s.Length; x++){
            schematic[line, x] = s[x];
        }
        line++;
    }
}

int sum = 0;

List<char> soFar = [];
bool part = false;

for (int i = 0; i < size; i++){
    for (int j = 0; j < size; j++){
        if(numbers.Contains(schematic[i,j])){
            soFar.Add(schematic[i,j]);
            part |= i-1>=0 && symbols.Contains(schematic[i-1,j]) || i+1<size && symbols.Contains(schematic[i+1,j]) ||
                    j-1>=0 && symbols.Contains(schematic[i,j-1]) || j+1<size && symbols.Contains(schematic[i,j+1]) ||
                    i-1>=0 && j-1>=0 && symbols.Contains(schematic[i-1,j-1]) || i+1<size && j+1<size && symbols.Contains(schematic[i+1,j+1]) ||
                    i-1>=0 && j+1<size && symbols.Contains(schematic[i-1,j+1]) || i+1<size && j-1>=0 && symbols.Contains(schematic[i+1,j-1]);
        }else{
            if(part && soFar.Count > 0){
                Console.WriteLine("part:" + new string(soFar.ToArray()));
                sum += int.Parse(new string(soFar.ToArray()));
            }else if(soFar.Count > 0){
                Console.WriteLine("nopart:" + new string(soFar.ToArray()));
            }

            soFar.Clear();
            part = false;
        }
    }
}

Console.WriteLine(sum);

int gear = 0;
for (int i = 0; i < size; i++){
    for (int j = 0; j < size; j++){
        if(schematic[i,j] == '*'){
            HashSet<int> l = [];
            if(i-1>=0               && numbers.Contains(schematic[i-1,j])){     l.Add(numberAt(i-1,j, schematic, numbers));};
            if(i+1<size             && numbers.Contains(schematic[i+1,j])){     l.Add(numberAt(i+1,j, schematic, numbers));};
            if(j-1>=0               && numbers.Contains(schematic[i,j-1])){     l.Add(numberAt(i,j-1, schematic, numbers));};
            if(j+1<size             && numbers.Contains(schematic[i,j+1])){     l.Add(numberAt(i,j+1, schematic, numbers));};
            if(i-1>=0 && j-1>=0     && numbers.Contains(schematic[i-1,j-1])){   l.Add(numberAt(i-1,j-1, schematic, numbers));};
            if(i+1<size && j+1<size && numbers.Contains(schematic[i+1,j+1])){   l.Add(numberAt(i+1,j+1, schematic, numbers));};
            if(i-1>=0 && j+1<size   && numbers.Contains(schematic[i-1,j+1])){   l.Add(numberAt(i-1,j+1, schematic, numbers));};
            if(i+1<size && j-1>=0   && numbers.Contains(schematic[i+1,j-1])){   l.Add(numberAt(i+1,j-1, schematic, numbers));};
            Console.WriteLine(l.Count);
            Console.WriteLine(string.Join(",", l));
            if(l.Count==2){
                int[] a = l.ToArray();
                gear+= a[0]*a[1];
            }
        }
    }
}

Console.WriteLine(gear);

static int numberAt(int i,int j, char[,] schematic, HashSet<char> numbers){
    while(j-1>=0 && numbers.Contains(schematic[i,j-1])){
        j = j-1;
    }
    List<char> soFar = [];
    for (; j < schematic.Length; j++){
        if(numbers.Contains(schematic[i,j])){
            soFar.Add(schematic[i,j]);
        }else{
            break;
        }
    }
    return int.Parse(new string(soFar.ToArray()));
}