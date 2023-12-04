using System.Text.RegularExpressions;

const int SIZE = 204;

StreamReader reader = new StreamReader(File.OpenRead("./day04/input.txt")); 

int sum = 0;

int[] counts = new int[SIZE];
for (int i = 0; i < counts.Length; i++){
    counts[i]=1;
}
foreach (int i in counts){
    Console.WriteLine(i + "=" + counts[i]);
}

int row = 0;
while(reader.Peek() >= 0){
    string? s = reader.ReadLine();
    if(s is not null){
        s = s[8..];
        string[] t1 = s.Split(" | ");
        HashSet<string> winnings = new(t1[0].Split(" "));
        winnings.Remove("");
        HashSet<string> owns = new(t1[1].Split(" "));
        owns.Remove("");
        int count = 0;
        foreach(string own in owns){
            if(winnings.Contains(own)){
                count++;
            }
        }
        if(count>0){
            int res = 1;
            for (int i = 1; i < count; i++){
                res *= 2;
            }

            for (int i = 0; i < count; i++){
                if(row+1+i<SIZE){
                    counts[row+1+i] +=  counts[row];
                }
            }
            sum += res;
        }
        row++;
    }
}
Console.WriteLine(sum);

int total = 0;
foreach (int count in counts){
    total += count;
}
Console.WriteLine(total);