
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
GlobalParam gparam = new GlobalParam(1.456f);
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

```c#
GlobalParam gparam = new GlobalParam();
gparam.Add("R", 1.456f);

lSystem.AddRule("A", varCount: 0, g: gparam,
    condition: (MChar t) => true,
    func: (MChar c, GlobalParam g) => {
        return MChar.Char("F", 1) 
        + MChar.Open + MChar.Plus + MChar.Char("A") + MChar.Close 
        + MChar.Open + MChar.Minus + MChar.Char("A") + MChar.Close;
    });

lSystem.AddRule("F", varCount: 1, g: gparam,
    condition: (MChar t) => true,
    func: (MChar c, GlobalParam g) => {
        float R = g["R"], s = c[0];
        return MChar.Char("F", s * R).ToMString();
    });

MString axiom = MChar.Char("A").ToMString();
MString sentence = lSystem.Generate(axiom, 11);
```
![image](https://github.com/mekjh12/LSystem-Parametric/assets/122244587/d5955f49-bece-4ed1-92a9-d2df1f052569)

* Honda
```c#
 GlobalParam gparam = new GlobalParam();
 gparam.Add("r1", 0.9f);
 gparam.Add("r2", 0.6f);
 gparam.Add("a0", 45.0f);
 gparam.Add("a2", 45.0f);
 gparam.Add("d", 137.5f);
 gparam.Add("wr", 0.707f);

 lSystem.AddRule("A", varCount: 2, g: gparam,
     condition: (t) => true, func: (MChar c, GlobalParam g) =>
     { 
         float l = c[0], w = c[1];
         return MChar.Char("!", w) + MChar.Char("F", l)
         + MChar.Open + MChar.Char("&", g["a0"]) + MChar.Char("B", l * g["r2"], w * g["wr"]) + MChar.Close
         + MChar.Char("/", g["d"]) + MChar.Char("A", l * g["r1"], w * g["wr"]);
     });

 lSystem.AddRule("B", varCount: 2, g: gparam,
     condition: (t) => true, func: (MChar c, GlobalParam g) =>
     {
         float l = c[0], w = c[1];
         return MChar.Char("!", w) + MChar.Char("F", l)
         + MChar.Open + MChar.Char("-", g["a2"]) + MChar.Char("$") + MChar.Char("C", l * g["r2"], w * g["wr"]) + MChar.Close
         + MChar.Char("C", l * g["r1"], w * g["wr"]);
     });

 lSystem.AddRule("C", varCount: 2, g: gparam,
     condition: (t) => true, func: (MChar c, GlobalParam g) =>
     {
         float l = c[0], w = c[1];
         return MChar.Char("!", w) + MChar.Char("F", l)
         + MChar.Open + MChar.Char("+", g["a2"]) + MChar.Char("$") + MChar.Char("B", l * g["r2"], w * g["wr"]) + MChar.Close
         + MChar.Char("B", l * g["r1"], w * g["wr"]);
     });

 MString axiom = MChar.Char("A", 1, 10).ToMString();
 MString sentence = lSystem.Generate(axiom, 10);
```
![image](https://github.com/mekjh12/LSystem-Parametric/assets/122244587/171c36cc-ef93-4bf3-b377-e0c73f30bc40)


# Aono and Kunii
```c#
 GlobalParam gparam = new GlobalParam();
 gparam.Add("r1", 0.9f);
 gparam.Add("r2", 0.7f);
 gparam.Add("a1", 10.0f);
 gparam.Add("a2", 60.0f);
 gparam.Add("wr", 0.707f);

 lSystem.AddRule("A", varCount: 2, g: gparam,
     condition: (t) => true, func: (MChar c, GlobalParam g) =>
     {
         float l = c[0], w = c[1];
         return MChar.Char("!", w) + MChar.Char("F", l)
         + MChar.Open + MChar.Char("&", g["a1"]) + MChar.Char("B", l * g["r1"], w * g["wr"]) + MChar.Close
         + MChar.Char("/", 180)
         + MChar.Open + MChar.Char("&", g["a2"]) + MChar.Char("B", l * g["r2"], w * g["wr"]) + MChar.Close;
     });

 lSystem.AddRule("B", varCount: 2, g: gparam,
     condition: (t) => true, func: (MChar c, GlobalParam g) =>
     {
         float l = c[0], w = c[1];
         return MChar.Char("!", w) + MChar.Char("F", l)
         + MChar.Open + MChar.Char("+", g["a1"]) + MChar.Char("$") + MChar.Char("B", l * g["r1"], w * g["wr"]) + MChar.Close
         + MChar.Open + MChar.Char("-", g["a2"]) + MChar.Char("$") + MChar.Char("B", l * g["r2"], w * g["wr"]) + MChar.Close;
     });

 MString axiom = MChar.Char("A", 1, 10).ToMString();
 MString sentence = lSystem.Generate(axiom, 10);
```
![image](https://github.com/mekjh12/LSystem-Parametric/assets/122244587/d63c863c-423d-4eaf-b059-ecca7c799e25)

## Stochastic L-systems 3d that applies the thickness of tree branches

![image](https://github.com/mekjh12/LSystem-Stochastic/assets/122244587/15855fb1-b79b-43c2-b2cf-ab6c8c09eb1d)

## Stochastic L-systems 3d

![image](https://github.com/mekjh12/LSystem/assets/122244587/a21dbfcd-bd57-49bc-8867-411d06a68891)

## Edge ReWriting

![image](https://github.com/mekjh12/LSystem/assets/122244587/4d599def-9a53-4d15-952e-0a3c5916779c)

## Node ReWriting

![image](https://github.com/mekjh12/LSystem/assets/122244587/84b6572d-2454-4a52-bba2-6b6c452440cc)
