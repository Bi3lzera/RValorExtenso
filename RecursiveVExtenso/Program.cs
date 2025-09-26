using System.Diagnostics;

namespace VExtenso
{
    enum Unidade
    {
        um = 1,
        dois = 2,
        três = 3,
        quatro = 4,
        cinco = 5,
        seis = 6,
        sete = 7,
        oito = 8,
        nove = 9,
    }

    enum Dezenas
    {
        dez = 10,
        onze = 11,
        doze = 12,
        treze = 13,
        quatorze = 14,
        quinze = 15,
        dezesseis = 16,
        dezessete = 17,
        dezoito = 18,
        dezenove = 19,
        vinte = 2,
        trinta = 3,
        quarenta = 4,
        cinquenta = 5,
        sessenta = 6,
        setenta = 7,
        oitenta = 8,
        noventa = 9
    }

    enum Centenas
    {
        cem = 0,
        cento = 1,
        duzentos = 2,
        trezentos = 3,
        quatrocentos = 4,
        quinhentos = 5,
        seiscentos = 6,
        setecentos = 7,
        oitocentos = 8,
        novecentos = 9
    }

    enum Milhares
    {
        mil,
        milhão,
        trilhão,
        quatrilhão,
        quintilhão,
        sextilhão,
        septilhão,
        octilhão,
        nonalhão,
        dectalhão,
    }

    class VExtenso 
    {
        public string Converter(double num)
        {
            string text = string.Empty;
            try
            {
                string sNum = num.ToString().Split(',')[0];
                text += $"{ConvertToString(sNum)} reais";
                string sNumCents = "0" + num.ToString().Split(',')[1].Substring(0, 2);
                text += $" e {ConvertToString(sNumCents)} centavos";
            }
            catch { }

            return text;
        }

        public string ConvertToString(string sNum)
        {
            string text = string.Empty;
            sNum = Normalize(sNum);

            //
            //CENTENA
            //
            if (sNum.Substring(0, 1) != "0" && sNum.Substring(0, 3) != "100")
            {
                text += Enum.GetName(typeof(Centenas), Convert.ToInt32(sNum.Substring(0, 1))) + " e ";
            }else if(sNum.Substring(0, 3) == "100")
            {
                text += Enum.GetName(typeof(Centenas), 0) + " e ";
            }

            //
            //DEZENA
            //
            if (sNum.Substring(1, 1) != "0")
            {
                if (Convert.ToInt32(sNum.Substring(1, 2)) < 20 && Convert.ToInt32(sNum.Substring(1, 2)) >= 10)
                    text += Enum.GetName(typeof(Dezenas), Convert.ToInt32(sNum.Substring(1, 2))) + " e ";
                else
                    text += Enum.GetName(typeof(Dezenas), Convert.ToInt32(sNum.Substring(1, 1))) + " e ";
            }

            //
            //UNIDADE
            //
            if (sNum.Substring(2, 1) != "0" && (Convert.ToInt32(sNum.Substring(1, 2)) < 10 || Convert.ToInt32(sNum.Substring(1, 2)) > 20))
            {
                text += Enum.GetName(typeof(Unidade), Convert.ToInt32(sNum.Substring(2, 1))) + " ";
            }

            //
            //MILHAR
            //
            if (sNum.Length % 3 == 0 && sNum.Length > 3)
            {
                text.Remove(text.Length - 2, 2);
                if(Convert.ToInt32(sNum.Substring(2, 1)) > 1 && sNum.Length > 6)
                {
                    string tText = MilharPos(sNum).TrimEnd('ã', 'o');
                    tText += "ões";
                    text += tText + " e ";
                }
                else
                {
                    text += MilharPos(sNum);
                }               
            }

            //
            //Chama esta mesma função de forma recursiva para criar o restante do texto.
            //
            if (sNum.Length > 3)
            {
                text += ConvertToString(sNum.Substring(3)) + " e ";
            }            


            return text;
        }

        public string MilharPos (string num)
        {
            int t = (num.Length / 3 - num.Length % 3) - 2;
            return Enum.GetName(typeof(Milhares), t);
        }

        public string Normalize(string num)
        {
            int fNum = num.Length;
            if (fNum % 3 == 0) return num;
            if (fNum % 3 == 1) return "00" + num;
            if (fNum % 3 == 2) return "0" + num;
            return string.Empty;
        }
    }

    class App {
        public static void Main(string[] args)
        {
            VExtenso vExtenso = new VExtenso();
            while (true)
            {
                Console.WriteLine(vExtenso.Converter(Convert.ToDouble(Console.ReadLine())));
            }
        }
    }
}