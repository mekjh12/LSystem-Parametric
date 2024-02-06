
## Parametric LSystem
```C#
GlobalParam gparam = new GlobalParam(0.3f, 0.7f, (float)Math.Sqrt(0.21f));

lSystem.AddRule("F", varCount: 2, g: gparam,
    condition: (MChar t) => (t[1] == 0),
    func: (MChar c, GlobalParam g) => {
        float x = c[0], t = c[1];
        float p = g.Value[0], q = g.Value[1], h = g.Value[2];
        return MChar.Char("F", x * p, 2) + MChar.Char("+")
        + MChar.Char("F", x * h, 1) + MChar.Char("-") + MChar.Char("-")
        + MChar.Char("F", x * h, 1) + MChar.Char("+")
        + MChar.Char("F", x * q, 0);
    });

lSystem.AddRule("F", varCount: 2, g: gparam, 
    condition: (MChar t) => (t[1] > 0),
    func: (MChar c, GlobalParam g) => {
        return MChar.Char("F", c[0], c[1] - 1).ToMString();
    });

MString axiom = MChar.Char("F", 10, 0).ToMString();
MString sentence = lSystem.Generate(axiom, 10);
```
![image](https://github.com/mekjh12/LSystem-Parametric/assets/122244587/14c0a22f-3a6a-4dd7-8e65-cb7801e2eb4a)

```C#
lSystem.AddRule("A", varCount: 1, g: gparam,
    condition: (MChar t) => true,
    func: (MChar c, GlobalParam g) => {
        float R = g[0];
        float s = c[0];
        return MChar.Char("F", s) + MChar.Char("[") + MChar.Char("+")
        + MChar.Char("A", s / R) + MChar.Char("]") + MChar.Char("[") + MChar.Char("-")
        + MChar.Char("A", s / R) + MChar.Char("]");
    });
MString axiom = MChar.Char("A", 10).ToMString();
MString sentence = lSystem.Generate(axiom, 10);
```
![image](https://github.com/mekjh12/LSystem-Parametric/assets/122244587/367a1d2b-a77f-41d2-a84d-c1a978efa01a)

## Stochastic L-systems 3d that applies the thickness of tree branches

![image](https://github.com/mekjh12/LSystem-Stochastic/assets/122244587/15855fb1-b79b-43c2-b2cf-ab6c8c09eb1d)

## Stochastic L-systems 3d

![image](https://github.com/mekjh12/LSystem/assets/122244587/a21dbfcd-bd57-49bc-8867-411d06a68891)

## Edge ReWriting

![image](https://github.com/mekjh12/LSystem/assets/122244587/4d599def-9a53-4d15-952e-0a3c5916779c)

## Node ReWriting

![image](https://github.com/mekjh12/LSystem/assets/122244587/84b6572d-2454-4a52-bba2-6b6c452440cc)
