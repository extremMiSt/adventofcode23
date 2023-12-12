// ~~stolen~~ adapted from https://github.com/GassaFM/aoc2023/blob/main/day12/d12p2.d 
// with some explanations by the author in https://www.reddit.com/r/adventofcode/comments/18ge41g/comment/kd04bjg/

class Part2{ 
    static public long Solve2(string l){
        string[] split = l.Split(" ");
        string template = split[0];
        template = template + '?' + template + '?' + template + '?' + template + '?' + template + ".";
        template += '.';
        int[] pattern = split[1].Split(",").Select(int.Parse).ToArray();
        pattern = [.. pattern, .. pattern, .. pattern, .. pattern, .. pattern];
        int n = template.Length;
        int k = pattern.Length;

        long[,,] f = new long[n+1,k+2,n+2];
        f[0,0,0] = 1;
        for(int i = 0; i<n; i++){
            for(int j = 0; j<k+1; j++){
                for(int p = 0; p<n+1; p++){
                    long cur = f[i,j,p];
                    if(cur==0){
                        continue;
                    }
                    if((template[i]=='.' || template[i]=='?') && (p==0|| p==pattern[j-1])){
                        f[i+1,j,0] += cur;
                    }
                    if(template[i]=='#'|| template[i]=='?'){
                        f[i+1, j+(p==0?1:0), p+1] += cur;
                    }
                }
            }
        }
        return f[n,k,0];
    }
}