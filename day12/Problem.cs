class Problem{

    readonly string springs;
    readonly int[] sequence;

    public Problem(string line){
        string[] split = line.Split(" ");
        springs = split[0];
        sequence = split[1].Split(",").Select(int.Parse).ToArray();
    }

    public int Solve(){
        int count = 0;
        foreach(string s in Make([springs])){
            if(Check(s)){
                count++;
            }
        }
        return count;
    }

    string[] Make(string[] inp){
        int first = inp[0].IndexOf('?');
        if(first == -1){
            return inp;
        }
        string[] res = new string[inp.Length*2];
        for(int i = 0; i < inp.Length; i++){
            res[i*2]   = inp[i][..first] +'.'+inp[i][(first+1)..];
            res[i*2+1] = inp[i][..first] +'#'+inp[i][(first+1)..];
        }
        return Make(res);
    }

    bool Check(string s){
        List<int> pattern = [];
        int count = 0;
        foreach(char c in s){
            if(c == '#'){
                count++;
            }else if(count > 0){
                pattern.Add(count);
                count = 0;
            }
        }
        if(count > 0){
            pattern.Add(count);
        }
        if(pattern.Count == sequence.Length){
            bool ok = true;
            for(int i = 0; i < pattern.Count; i++){
                ok &= pattern[i] == sequence[i];
            }
            return ok;
        }else{
            return false;
        }
    }





}