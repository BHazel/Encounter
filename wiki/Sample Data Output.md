Samples of the exported data from Encounter use the _M18C6_KC.log_ file included in the source.

CSV
---

    Crown Ether [18]crown-6 with K+ guest Counterpoise
    DIMER BASIS /au
    Dimer,-951.133294
    Monomer A,-922.840067
    Monomer B,-28.149984
    MONOMER BASIS /au
    Monomer A,-922.832002
    Monomer B,-28.149955
    INTERACTION ENERGY
    /au,-0.143243
    /kJ/mol,-376.085311
    BINDING CONSTANT
    /1,8.397086e+65

JSON
----

    {
        "Description": "Crown Ether [18]crown-6 with K+ guest Counterpoise",
        "Basis": [
            {
                "Type": "Dimer",
                "Dimer": "-951.133294",
                "MonomerA": "-922.840067",
                "MonomerB": "-28.149984"
            },
            {
                "Type": "Monomer",
                "MonomerA": "-922.832002",
                "MonomerB": "-28.149955"
            }
        ],
        "InteractionEnergy": {
            "Hartree": "-0.143243",
            "Kjmol": "-376.085311"
        },
        "BindingConstant": "8.397086e+65"
    }

XML
---

    <?xml version="1.0" encoding="utf-8"?>
    <enc:Counterpoise xmlns:enc="http://encounter.codeplex.com" enc:Description="Crown Ether [18]crown-6 with K+ guest Counterpoise">
        <enc:Basis enc:Type="Dimer">
            <enc:Dimer>-951.133294</enc:Dimer>
            <enc:MonomerA>-922.840067</enc:MonomerA>
            <enc:MonomerB>-28.149984</enc:MonomerB>
        </enc:Basis>
        <enc:Basis enc:Type="Monomer">
            <enc:MonomerA>-922.832002</enc:MonomerA>
            <enc:MonomerB>-28.149955</enc:MonomerB>
        </enc:Basis>
        <enc:InteractionEnergy>
            <enc:Hartree>-0.143243</enc:Hartree>
            <enc:Kjmol>-376.085311</enc:Kjmol>
        </enc:InteractionEnergy>
        <enc:BindingConstant>8.397086e+65</enc:BindingConstant>
    </enc:Counterpoise>