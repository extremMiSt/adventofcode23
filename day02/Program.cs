using System.Text.RegularExpressions;

//task1
StreamReader reader = new StreamReader(File.OpenRead("./day02/example1.txt")); 

int sum = 0;

while(reader.Peek() >= 0){
    string? s = reader.ReadLine();
    if(s is not null){
        int id = int.Parse(s.Substring(5, s.IndexOf(':')-5));
        Console.WriteLine(id);
        string[] games = s.Substring(s.IndexOf(": ")+2).Split("; ");
        bool possible = true;
        foreach(string game in games){
            string[] cubes = game.Split(", ");
            foreach(string cube in cubes){
                string[] split = cube.Split(" ");
                int num = int.Parse(split[0]);
                if(split[1]=="red" && num > 12){
                    possible = false;
                    break;
                }else if(split[1]=="green" && num > 13){
                    possible = false;
                    break;
                }else if(split[1]=="blue" && num > 14){
                    possible = false;
                    break;
                }
            }
            if(!possible){
                break;
            }
        }
        if(possible){
            sum += id;
        }
    }
}
Console.WriteLine("task1: " + sum);

//task1
StreamReader reader2 = new StreamReader(File.OpenRead("./day02/input.txt")); 

int power = 0;

while(reader2.Peek() >= 0){
    string? s = reader2.ReadLine();
    if(s is not null){
        int id = int.Parse(s.Substring(5, s.IndexOf(':')-5));
        Console.WriteLine(id);
        string[] games = s.Substring(s.IndexOf(": ")+2).Split("; ");
        int[] min = [0,0,0];
        foreach(string game in games){
            string[] cubes = game.Split(", ");
            foreach(string cube in cubes){
                string[] split = cube.Split(" ");
                int num = int.Parse(split[0]);
                if(split[1]=="red" && num > min[0]){
                    min[0] = num;
                }
                if(split[1]=="green" && num > min[1]){
                    min[1] = num;
                }
                if(split[1]=="blue" && num > min[2]){
                    min[2] = num;
                }
            }
        }
        Console.WriteLine(min[0]*min[1]*min[2]);
        power += min[0]*min[1]*min[2];
    }
}
Console.WriteLine("task2: " + power);