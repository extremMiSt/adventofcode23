using System.Text.RegularExpressions;

StreamReader reader = new StreamReader(File.OpenRead("./day06/input.txt")); 
int[,] races = new int[0,0];
string[] one = ["", ""];
bool init = false;

int line = 0;
while(reader.Peek() >= 0){
    string? s = reader.ReadLine();
    if(s is not null){
        string[] tokens = Regex.Split(s, @"\s+");
        if(!init){
            races = new int[2,tokens.Length-1];
            init = true;
        }
        for (int i = 0; i < races.GetLength(1); i++)        {
            races[line, i] = int.Parse(tokens[i+1]);
            one[line] += tokens[i+1];
        }
        line++;
    }
}

int total = 1;
for (int i = 0; i < races.GetLength(1); i++){
    int c = 0;
    for (int j = 0; j <= races[0,i]; j++){
        if(Dist(races[0,i], j) > races[1,i] ){
            c++;
        }
    }
    total *= c;
}
Console.WriteLine(total);

total = 0;
long time = long.Parse(one[0]);
long dist = long.Parse(one[1]);
for (long j = 0; j <= time; j++){
    if(Dist(time, j) > dist ){
        total++;
    }
}
Console.WriteLine(total);

static long Dist(long time, long hold){
    return hold * (time-hold);
}
