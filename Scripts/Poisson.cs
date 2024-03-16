using UnityEngine;

public class Poisson
{
    // For Code, see https://www.johndcook.com/blog/csharp_poisson/
    public static float LogFactorial(float x){
        float e = Mathf.Exp(1);
        float fact = Mathf.Pow(Mathf.Sqrt(Mathf.PI)*(x/e),x);
        fact *= Mathf.Pow((((8*x + 4)*x + 1)*x + 1/30.0f),(1.0f/6.0f));
        return fact;
    }

    public static float GetUniform()
    {
        return Random.Range(0,1);
    }

    public static int GetPoisson(float lambda)
    {
        return (lambda < 30.0) ? PoissonSmall(lambda) : PoissonLarge(lambda);
    }

    private static int PoissonSmall(float lambda)
    {
        double p = 1.0, L = Mathf.Exp(-lambda);
        int k = 0;
        do
        {
            k++;
            p *= GetUniform();
        }
        while (p > L);
        return k - 1;
    }

    private static int PoissonLarge(float lambda)
    {
        float c = 0.767f - 3.36f/lambda;
        float beta = Mathf.PI/Mathf.Sqrt(3.0f*lambda);
        float alpha = beta*lambda;
        float k = Mathf.Log(c) - lambda - Mathf.Log(beta);

        for(;;)
        {
            float u = GetUniform();
            float x = (alpha - Mathf.Log((1.0f - u)/u))/beta;
            int n = (int) Mathf.Floor(x + 0.5f);
            if (n < 0)
                continue;
            float v = GetUniform();
            float y = alpha - beta*x;
            float temp = 1.0f + Mathf.Exp(y);
            float lhs = y + Mathf.Log(v/(temp*temp));
            float rhs = k + n*Mathf.Log(lambda) - LogFactorial(n);
            if (lhs <= rhs)
                return n;
        }
    }

}
