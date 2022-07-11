
using System;

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

using iTin.Core.ComponentModel;

namespace iTin.Core.Models.Design.Enums
{
    /// <summary>
    /// Known cultures.
    /// </summary>
    [Serializable]
    [JsonConverter(typeof(StringEnumConverter))]
    public enum KnownCulture
    {
        /// <summary>
        /// Current culture
        /// </summary>
        Current,

        /// <summary>
        /// Afrikaans
        /// </summary>
        [EnumDescription("af")]
        af,

        /// <summary>
        /// Afrikaans (South Africa)
        /// </summary>
        [EnumDescription("af-ZA")]
        afZA,

        /// <summary>
        /// Amharic
        /// </summary>
        [EnumDescription("am")]
        am,

        /// <summary>
        /// Amharic (Ethiopia)
        /// </summary>
        [EnumDescription("am-ET")]
        amET,

        /// <summary>
        /// Arabic
        /// </summary>
        [EnumDescription("ar")]
        ar,

        /// <summary>
        /// Arabic (Saudi Arabia)
        /// </summary>
        [EnumDescription("ar-SA")]
        arSA,

        /// <summary>
        /// Arabic (Iraq)
        /// </summary>
        [EnumDescription("ar-IQ")]
        arIQ,

        /// <summary>
        /// Arabic (Egypt)
        /// </summary>
        [EnumDescription("ar-EG")]
        arEG,

        /// <summary>
        /// Arabic (Libya)
        /// </summary>
        [EnumDescription("ar-LY")]
        arLY,

        /// <summary>
        /// Arabic (Algeria)
        /// </summary>
        [EnumDescription("ar-DZ")]
        arDZ,

        /// <summary>
        /// Arabic (Morocco)
        /// </summary>
        [EnumDescription("ar-MA")]
        arMA,

        /// <summary>
        /// Arabic (Tunisia)
        /// </summary>
        [EnumDescription("ar-TN")]
        arTN,

        /// <summary>
        /// Arabic (Oman)
        /// </summary>
        [EnumDescription("ar-OM")]
        arOM,

        /// <summary>
        /// Arabic (Yemen)
        /// </summary>
        [EnumDescription("ar-YE")]
        arYE,

        /// <summary>
        /// Arabic (Syria)
        /// </summary>
        [EnumDescription("ar-SY")]
        arSY,

        /// <summary>
        /// Arabic (Jordan)
        /// </summary>
        [EnumDescription("ar-JO")]
        arJO,

        /// <summary>
        /// Arabic (Lebanon)
        /// </summary>
        [EnumDescription("ar-LB")]
        arLB,

        /// <summary>
        /// Arabic (Kuwait)
        /// </summary>
        [EnumDescription("ar-KW")]
        arKW,

        /// <summary>
        /// Arabic (U.A.E.)
        /// </summary>
        [EnumDescription("ar-AE")]
        arAE,

        /// <summary>
        /// Arabic (Bahrain)
        /// </summary>
        [EnumDescription("ar-BH")]
        arBH,

        /// <summary>
        /// Arabic (Qatar)
        /// </summary>
        [EnumDescription("ar-QA")]
        arQA,

        /// <summary>
        /// Mapudungun
        /// </summary>
        [EnumDescription("arn")]
        arn,

        /// <summary>
        /// Mapudungun (Chile)
        /// </summary>
        [EnumDescription("arn-CL")]
        arnCL,

        /// <summary>
        /// Assamese
        /// </summary>
        [EnumDescription("as")]
        @as,

        /// <summary>
        /// Assamese (India)
        /// </summary>
        [EnumDescription("as-IN")]
        asIN,

        /// <summary>
        /// Azeri
        /// </summary>
        [EnumDescription("az")]
        az,

        /// <summary>
        /// Azeri (Latin, Azerbaijan)
        /// </summary>
        [EnumDescription("az-Latn-AZ")]
        azLatnAZ,

        /// <summary>
        /// Azeri (Cyrillic, Azerbaijan)
        /// </summary>
        [EnumDescription("az-Cyrl-AZ")]
        azCyrlAZ,

        /// <summary>
        /// Azeri (Cyrillic)
        /// </summary>
        [EnumDescription("az-Cyrl")]
        azCyrl,

        /// <summary>
        /// Azeri (Latin)
        /// </summary>
        [EnumDescription("az-Latn")]
        azLatn,

        /// <summary>
        /// Bashkir
        /// </summary>
        [EnumDescription("ba")]
        ba,

        /// <summary>
        /// Bashkir (Russia)
        /// </summary>
        [EnumDescription("ba-RU")]
        baRU,

        /// <summary>
        /// Belarusian
        /// </summary>
        [EnumDescription("be")]
        be,

        /// <summary>
        /// Belarusian (Belarus)
        /// </summary>
        [EnumDescription("be-BY")]
        beBY,

        /// <summary>
        /// Bulgarian
        /// </summary>
        [EnumDescription("bg")]
        bg,

        /// <summary>
        /// Bulgarian (Bulgaria)
        /// </summary>
        [EnumDescription("bg-BG")]
        bgBG,

        /// <summary>
        /// Bengali
        /// </summary>
        [EnumDescription("bn")]
        bn,

        /// <summary>
        /// Bengali (India)
        /// </summary>
        [EnumDescription("bn-IN")]
        bnIN,

        /// <summary>
        /// Bengali (Bangladesh)
        /// </summary>
        [EnumDescription("bn-BD")]
        bnBD,

        /// <summary>
        /// Tibetan
        /// </summary>
        [EnumDescription("bo")]
        bo,

        /// <summary>
        /// Tibetan (PRC)
        /// </summary>
        [EnumDescription("bo-CN")]
        boCN,

        /// <summary>
        /// Breton
        /// </summary>
        [EnumDescription("br")]
        br,

        /// <summary>
        /// Breton (France)
        /// </summary>
        [EnumDescription("br-FR")]
        brFR,

        /// <summary>
        /// Bosnian (Latin, Bosnia and Herzegovina)
        /// </summary>
        [EnumDescription("bs-Latn-BA")]
        bsLatnBA,

        /// <summary>        
        /// Bosnian (Cyrillic, Bosnia and Herzegovina)
        /// </summary>
        [EnumDescription("bs-Cyrl-BA")]
        bsCyrlBA,

        /// <summary>
        /// Bosnian (Cyrillic)
        /// </summary>
        [EnumDescription("bs-Cyrl")]
        bsCyrl,

        /// <summary>
        /// Bosnian (Latin)
        /// </summary>
        [EnumDescription("bs-Latn")]
        bsLatn,

        /// <summary>
        /// Bosnian
        /// </summary>
        [EnumDescription("bs")]
        bs,

        /// <summary>
        /// Catalan
        /// </summary>
        [EnumDescription("ca")]
        ca,

        /// <summary>
        /// Catalan (Catalan)
        /// </summary>
        [EnumDescription("ca-ES")]
        caES,

        /// <summary>
        /// Corsican
        /// </summary>
        [EnumDescription("co")]
        co,

        /// <summary>
        /// Corsican (France)
        /// </summary>
        [EnumDescription("co-FR")]
        coFR,

        /// <summary>
        /// Czech
        /// </summary>
        [EnumDescription("cs")]
        cs,

        /// <summary>
        /// Czech (Czech Republic)
        /// </summary>
        [EnumDescription("cs-CZ")]
        csCZ,

        /// <summary>
        /// Welsh
        /// </summary>
        [EnumDescription("cy")]
        cy,

        /// <summary>
        /// Welsh (United Kingdom)
        /// </summary>
        [EnumDescription("cy-GB")]
        cyGB,

        /// <summary>
        /// Danish
        /// </summary>
        [EnumDescription("da")]
        da,

        /// <summary>
        /// Danish (Denmark)
        /// </summary>
        [EnumDescription("da-DK")]
        daDK,

        /// <summary>
        /// German
        /// </summary>
        [EnumDescription("de")]
        de,

        /// <summary>
        /// German (Germany)
        /// </summary>
        [EnumDescription("de-DE")]
        deDE,

        /// <summary>
        /// German (Switzerland)
        /// </summary>
        [EnumDescription("de-CH")]
        deCH,

        /// <summary>
        /// German (Austria)
        /// </summary>
        [EnumDescription("de-AT")]
        deAT,

        /// <summary>
        /// German (Luxembourg)
        /// </summary>
        [EnumDescription("de-LU")]
        deLU,

        /// <summary>
        /// German (Liechtenstein)
        /// </summary>
        [EnumDescription("de-LI")]
        deLI,

        /// <summary>
        /// Lower Sorbian (Germany)
        /// </summary>
        [EnumDescription("dsb-DE")]
        dsbDE,

        /// <summary>
        /// Lower Sorbian
        /// </summary>
        [EnumDescription("dsb")]
        dsb,

        /// <summary>
        /// Divehi
        /// </summary>
        [EnumDescription("dv")]
        dv,

        /// <summary>
        /// Divehi (Maldives)
        /// </summary>
        [EnumDescription("dv-MV")]
        dvMV,

        /// <summary>
        /// Greek
        /// </summary>
        [EnumDescription("el")]
        el,

        /// <summary>
        /// Greek (Greece)
        /// </summary>
        [EnumDescription("el-GR")]
        elGR,

        /// <summary>
        /// English
        /// </summary>
        [EnumDescription("en")]
        en,

        /// <summary>
        /// English (United States)
        /// </summary>
        [EnumDescription("en-US")]
        enUS,

        /// <summary>
        /// English (United Kingdom)
        /// </summary>
        [EnumDescription("en-GB")]
        enGB,

        /// <summary>
        /// English (Australia)
        /// </summary>
        [EnumDescription("en-AU")]
        enAU,

        /// <summary>
        /// English (Canada)
        /// </summary>
        [EnumDescription("en-CA")]
        enCA,

        /// <summary>
        /// English (New Zealand)
        /// </summary>
        [EnumDescription("en-NZ")]
        enNZ,

        /// <summary>
        /// English (Ireland)
        /// </summary>
        [EnumDescription("en-IE")]
        enIE,

        /// <summary>
        /// English (South Africa)
        /// </summary>
        [EnumDescription("en-ZA")]
        enZA,

        /// <summary>
        /// English (Jamaica)
        /// </summary>
        [EnumDescription("en-JM")]
        enJM,

        /// <summary>
        /// English (Caribbean)
        /// </summary>
        [EnumDescription("en-029")]
        en029,

        /// <summary>
        /// English (Belize)
        /// </summary>
        [EnumDescription("en-BZ")]
        enBZ,

        /// <summary>
        /// English (Trinidad and Tobago)
        /// </summary>
        [EnumDescription("en-TT")]
        enTT,

        /// <summary>
        /// English (Zimbabwe)
        /// </summary>
        [EnumDescription("en-ZW")]
        enZW,

        /// <summary>
        /// English (Republic of the Philippines)
        /// </summary>
        [EnumDescription("en-PH")]
        enPH,

        /// <summary>
        /// English (India)
        /// </summary>
        [EnumDescription("en-IN")]
        enIN,

        /// <summary>
        /// English (Malaysia)
        /// </summary>
        [EnumDescription("en-MY")]
        enMY,

        /// <summary>
        /// English (Singapore)
        /// </summary>
        [EnumDescription("en-SG")]
        enSG,

        /// <summary>
        /// Spanish
        /// </summary>
        [EnumDescription("es")]
        es,

        /// <summary>
        /// Spanish (Mexico)
        /// </summary>
        [EnumDescription("es-MX")]
        esMX,

        /// <summary>
        /// Spanish (Spain, International Sort)
        /// </summary>
        [EnumDescription("es-ES")]
        esES,

        /// <summary>
        /// Spanish (Guatemala)
        /// </summary>
        [EnumDescription("es-GT")]
        esGT,

        /// <summary>
        /// Spanish (Costa Rica)
        /// </summary>
        [EnumDescription("es-CR")]
        esCR,

        /// <summary>
        /// Spanish (Panama)
        /// </summary>
        [EnumDescription("es-PA")]
        esPA,

        /// <summary>
        /// Spanish (Dominican Republic)
        /// </summary>
        [EnumDescription("es-DO")]
        esDO,

        /// <summary>
        /// Spanish (Bolivarian Republic of Venezuela)
        /// </summary>
        [EnumDescription("es-VE")]
        esVE,

        /// <summary>
        /// Spanish (Colombia)
        /// </summary>
        [EnumDescription("es-CO")]
        esCO,

        /// <summary>
        /// Spanish (Peru)
        /// </summary>
        [EnumDescription("es-PE")]
        esPE,

        /// <summary>
        /// Spanish (Argentina)
        /// </summary>
        [EnumDescription("es-AR")]
        esAR,

        /// <summary>
        /// Spanish (Ecuador)
        /// </summary>
        [EnumDescription("es-EC")]
        esEC,

        /// <summary>
        /// Spanish (Chile)
        /// </summary>
        [EnumDescription("es-CL")]
        esCL,

        /// <summary>
        /// Spanish (Uruguay)
        /// </summary>
        [EnumDescription("es-UY")]
        esUY,

        /// <summary>
        /// Spanish (Paraguay)
        /// </summary>
        [EnumDescription("es-PY")]
        esPY,

        /// <summary>
        /// Spanish (Bolivia)
        /// </summary>
        [EnumDescription("es-BO")]
        esBO,

        /// <summary>
        /// Spanish (El Salvador)
        /// </summary>
        [EnumDescription("es-SV")]
        esSV,

        /// <summary>
        /// Spanish (Honduras)
        /// </summary>
        [EnumDescription("es-HN")]
        esHN,

        /// <summary>
        /// Spanish (Nicaragua)
        /// </summary>
        [EnumDescription("es-NI")]
        esNI,

        /// <summary>
        /// Spanish (Puerto Rico)
        /// </summary>
        [EnumDescription("es-PR")]
        esPR,

        /// <summary>
        /// Spanish (United States)
        /// </summary>
        [EnumDescription("es-US")]
        esUS,

        /// <summary>
        /// Estonian
        /// </summary>
        [EnumDescription("et")]
        et,

        /// <summary>
        /// Estonian (Estonia)
        /// </summary>
        [EnumDescription("et-EE")]
        etEE,

        /// <summary>
        /// Basque
        /// </summary>
        [EnumDescription("eu")]
        eu,

        /// <summary>
        /// Basque (Basque)
        /// </summary>
        [EnumDescription("eu-ES")]
        euES,

        /// <summary>
        /// Persian
        /// </summary>
        [EnumDescription("fa")]
        fa,

        /// <summary>
        /// Persian
        /// </summary>
        [EnumDescription("fa-IR")]
        faIR,

        /// <summary>
        /// Finnish
        /// </summary>
        [EnumDescription("fi")]
        fi,

        /// <summary>
        /// Finnish (Finland)
        /// </summary>
        [EnumDescription("fi-FI")]
        fiFI,

        /// <summary>
        /// Filipino
        /// </summary>
        [EnumDescription("fil")]
        fil,

        /// <summary>
        /// Filipino (Philippines)
        /// </summary>
        [EnumDescription("fil-PH")]
        filPH,

        /// <summary>
        /// Faroese
        /// </summary>
        [EnumDescription("fo")]
        fo,

        /// <summary>
        /// Faroese (Faroe Islands)
        /// </summary>
        [EnumDescription("fo-FO")]
        foFO,

        /// <summary>
        /// French
        /// </summary>
        [EnumDescription("fr")]
        fr,

        /// <summary>
        /// French (France)
        /// </summary>
        [EnumDescription("fr-FR")]
        frFR,

        /// <summary>
        /// French (Belgium)
        /// </summary>
        [EnumDescription("fr-BE")]
        frBE,

        /// <summary>
        /// French (Canada)
        /// </summary>
        [EnumDescription("fr-CA")]
        frCA,

        /// <summary>
        /// French (Switzerland)
        /// </summary>
        [EnumDescription("fr-CH")]
        frCH,

        /// <summary>
        /// French (Luxembourg)
        /// </summary>
        [EnumDescription("fr-LU")]
        frLU,

        /// <summary>
        /// French (Monaco)
        /// </summary>
        [EnumDescription("fr-MC")]
        frMC,

        /// <summary>
        /// Frisian
        /// </summary>
        [EnumDescription("fy")]
        fy,

        /// <summary>
        /// Frisian (Netherlands)
        /// </summary>
        [EnumDescription("fy-NL")]
        fyNL,

        /// <summary>
        /// Irish
        /// </summary>
        [EnumDescription("ga")]
        ga,

        /// <summary>
        /// Irish (Ireland)
        /// </summary>
        [EnumDescription("ga-IE")]
        gaIE,

        /// <summary>
        /// Scottish Gaelic
        /// </summary>
        [EnumDescription("gd")]
        gd,

        /// <summary>
        /// Scottish Gaelic (United Kingdom)
        /// </summary>
        [EnumDescription("gd-GB")]
        gdGB,

        /// <summary>
        /// Galician
        /// </summary>
        [EnumDescription("gl")]
        gl,

        /// <summary>
        /// Galician (Galician)
        /// </summary>
        [EnumDescription("gl-ES")]
        glES,

        /// <summary>
        /// Alsatian
        /// </summary>
        [EnumDescription("gsw")]
        gsw,

        /// <summary>
        /// Alsatian (France)
        /// </summary>
        [EnumDescription("gsw-FR")]
        gswFR,

        /// <summary>
        /// Gujarati
        /// </summary>
        [EnumDescription("gu")]
        gu,

        /// <summary>
        /// Gujarati (India)
        /// </summary>
        [EnumDescription("gu-IN")]
        guIN,

        /// <summary>
        /// Hausa
        /// </summary>
        [EnumDescription("ha")]
        ha,

        /// <summary>
        /// Hausa (Latin, Nigeria)
        /// </summary>
        [EnumDescription("ha-Latn-NG")]
        haLatnNG,

        /// <summary>
        /// Hausa (Latin)
        /// </summary>
        [EnumDescription("ha-Latn")]
        haLatn,

        /// <summary>
        /// Hebrew
        /// </summary>
        [EnumDescription("he")]
        he,

        /// <summary>
        /// Hebrew (Israel)
        /// </summary>
        [EnumDescription("he-IL")]
        heIL,

        /// <summary>
        /// Hindi
        /// </summary>
        [EnumDescription("hi")]
        hi,

        /// <summary>
        /// Hindi (India)
        /// </summary>
        [EnumDescription("hi-IN")]
        hiIN,

        /// <summary>
        /// Croatian
        /// </summary>
        [EnumDescription("hr")]
        hr,

        /// <summary>
        /// Croatian (Croatia)
        /// </summary>
        [EnumDescription("hr-HR")]
        hrHR,

        /// <summary>
        /// Croatian (Latin, Bosnia and Herzegovina)
        /// </summary>
        [EnumDescription("hr-BA")]
        hrBA,

        /// <summary>
        /// Upper Sorbian
        /// </summary>
        [EnumDescription("hsb")]
        hsb,

        /// <summary>
        /// Upper Sorbian (Germany)
        /// </summary>
        [EnumDescription("hsb-DE")]
        hsbDE,

        /// <summary>
        /// Hungarian
        /// </summary>
        [EnumDescription("hu")]
        hu,

        /// <summary>
        /// Hungarian (Hungary)
        /// </summary>
        [EnumDescription("hu-HU")]
        huHU,

        /// <summary>
        /// Armenian
        /// </summary>
        [EnumDescription("hy")]
        hy,

        /// <summary>
        /// Armenian (Armenia)
        /// </summary>
        [EnumDescription("hy-AM")]
        hyAM,

        /// <summary>
        /// Indonesian
        /// </summary>
        [EnumDescription("id")]
        id,

        /// <summary>
        /// Indonesian (Indonesia)
        /// </summary>
        [EnumDescription("id-ID")]
        idID,

        /// <summary>
        /// Igbo
        /// </summary>
        [EnumDescription("ig")]
        ig,

        /// <summary>
        /// Igbo (Nigeria)
        /// </summary>
        [EnumDescription("ig-NG")]
        igNG,

        /// <summary>
        /// Yi
        /// </summary>
        [EnumDescription("ii")]
        ii,

        /// <summary>
        /// Yi (PRC)
        /// </summary>
        [EnumDescription("ii-CN")]
        iiCN,

        /// <summary>
        /// Icelandic
        /// </summary>
        [EnumDescription("is")]
        @is,

        /// <summary>
        /// Icelandic (Iceland)
        /// </summary>
        [EnumDescription("is-IS")]
        isIS,

        /// <summary>
        /// Italian
        /// </summary>
        [EnumDescription("it")]
        it,

        /// <summary>
        /// Italian (Italy)
        /// </summary>
        [EnumDescription("it-IT")]
        itIT,

        /// <summary>
        /// Italian (Switzerland)
        /// </summary>
        [EnumDescription("it-CH")]
        itCH,

        /// <summary>
        /// Inuktitut
        /// </summary>
        [EnumDescription("iu")]
        iu,

        /// <summary>
        /// Inuktitut (Syllabics, Canada)
        /// </summary>
        [EnumDescription("iu-Cans-CA")]
        iuCansCA,

        /// <summary>
        /// Inuktitut (Latin, Canada)
        /// </summary>
        [EnumDescription("iu-Latn-CA")]
        iuLatnCA,

        /// <summary>
        /// Inuktitut (Syllabics)
        /// </summary>
        [EnumDescription("iu-Cans")]
        iuCans,

        /// <summary>
        /// Inuktitut (Latin)
        /// </summary>
        [EnumDescription("iu-Latn")]
        iuLatn,

        /// <summary>
        /// Japanese
        /// </summary>
        [EnumDescription("ja")]
        ja,

        /// <summary>
        /// Japanese (Japan)
        /// </summary>
        [EnumDescription("ja-JP")]
        jaJP,

        /// <summary>
        /// Georgian
        /// </summary>
        [EnumDescription("ka")]
        ka,

        /// <summary>
        /// Georgian (Georgia)
        /// </summary>
        [EnumDescription("ka-GE")]
        kaGE,

        /// <summary>
        /// Kazakh
        /// </summary>
        [EnumDescription("kk")]
        kk,

        /// <summary>
        /// Kazakh (Kazakhstan)
        /// </summary>
        [EnumDescription("kk-KZ")]
        kkKZ,

        /// <summary>
        /// Greenlandic
        /// </summary>
        [EnumDescription("kl")]
        kl,

        /// <summary>
        /// Greenlandic (Greenland)
        /// </summary>
        [EnumDescription("kl-GL")]
        klGL,

        /// <summary>
        /// Khmer
        /// </summary>
        [EnumDescription("km")]
        km,

        /// <summary>
        /// Khmer (Cambodia)
        /// </summary>
        [EnumDescription("km-KH")]
        kmKH,

        /// <summary>
        /// Kannada
        /// </summary>
        [EnumDescription("kn")]
        kn,

        /// <summary>
        /// Kannada (India)
        /// </summary>
        [EnumDescription("kn-IN")]
        knIN,

        /// <summary>
        /// Korean
        /// </summary>
        [EnumDescription("ko")]
        ko,

        /// <summary>
        /// Korean (Korea)
        /// </summary>
        [EnumDescription("ko-KR")]
        koKR,

        /// <summary>
        /// Konkani
        /// </summary>
        [EnumDescription("kok")]
        kok,

        /// <summary>
        /// Konkani (India)
        /// </summary>
        [EnumDescription("kok-IN")]
        kokIN,

        /// <summary>
        /// Kyrgyz
        /// </summary>
        [EnumDescription("ky")]
        ky,

        /// <summary>
        /// Kyrgyz (Kyrgyzstan)
        /// </summary>
        [EnumDescription("ky-KG")]
        kyKG,

        /// <summary>
        /// Luxembourgish
        /// </summary>
        [EnumDescription("lb")]
        lb,

        /// <summary>
        /// Luxembourgish (Luxembourg)
        /// </summary>
        [EnumDescription("lb-LU")]
        lbLU,

        /// <summary>
        /// Lao
        /// </summary>
        [EnumDescription("lo")]
        lo,

        /// <summary>
        /// Lao (Lao P.D.R.)
        /// </summary>
        [EnumDescription("lo-LA")]
        loLA,

        /// <summary>
        /// Lithuanian
        /// </summary>
        [EnumDescription("lt")]
        lt,

        /// <summary>
        /// Lithuanian (Lithuania)
        /// </summary>
        [EnumDescription("lt-LT")]
        ltLT,

        /// <summary>
        /// Latvian
        /// </summary>
        [EnumDescription("lv")]
        lv,

        /// <summary>
        /// Latvian (Latvia)
        /// </summary>
        [EnumDescription("lv-LV")]
        lvLV,

        /// <summary>
        /// Maori
        /// </summary>
        [EnumDescription("mi")]
        mi,

        /// <summary>
        /// Maori (New Zealand)
        /// </summary>
        [EnumDescription("mi-NZ")]
        miNZ,

        /// <summary>
        /// Macedonian (FYROM)
        /// </summary>
        [EnumDescription("mk")]
        mk,

        /// <summary>
        /// Macedonian (Former Yugoslav Republic of Macedonia)
        /// </summary>
        [EnumDescription("mk-MK")]
        mkMK,

        /// <summary>
        /// Malayalam
        /// </summary>
        [EnumDescription("ml")]
        ml,

        /// <summary>
        /// Malayalam (India)
        /// </summary>
        [EnumDescription("ml-IN")]
        mlIN,

        /// <summary>
        /// Mongolian
        /// </summary>
        [EnumDescription("mn")]
        mn,

        /// <summary>
        /// Mongolian (Cyrillic, Mongolia)
        /// </summary>
        [EnumDescription("mn-MN")]
        mnMN,

        /// <summary>
        /// Mongolian (Traditional Mongolian, PRC)
        /// </summary>
        [EnumDescription("mn-Mong-CN")]
        mnMongCN,

        /// <summary>
        /// Mongolian (Cyrillic)
        /// </summary>
        [EnumDescription("mn-Cyrl")]
        mnCyrl,

        /// <summary>
        /// Mongolian (Traditional Mongolian)
        /// </summary>
        [EnumDescription("mn-Mong")]
        mnMong,

        /// <summary>
        /// Mohawk
        /// </summary>
        [EnumDescription("moh")]
        moh,

        /// <summary>
        /// Mohawk (Mohawk)
        /// </summary>
        [EnumDescription("moh-CA")]
        mohCA,

        /// <summary>
        /// Marathi
        /// </summary>
        [EnumDescription("mr")]
        mr,

        /// <summary>
        /// Marathi (India)
        /// </summary>
        [EnumDescription("mr-IN")]
        mrIN,

        /// <summary>
        /// Malay
        /// </summary>
        [EnumDescription("ms")]
        ms,

        /// <summary>
        /// Malay (Malaysia)
        /// </summary>
        [EnumDescription("ms-MY")]
        msMY,

        /// <summary>
        /// Malay (Brunei Darussalam)
        /// </summary>
        [EnumDescription("ms-BN")]
        msBN,

        /// <summary>
        /// Maltese
        /// </summary>
        [EnumDescription("mt")]
        mt,

        /// <summary>
        /// Maltese (Malta)
        /// </summary>
        [EnumDescription("mt-MT")]
        mtMT,

        /// <summary>
        /// Norwegian
        /// </summary>
        [EnumDescription("no")]
        no,

        /// <summary>
        /// Norwegian, Bokmål (Norway)
        /// </summary>
        [EnumDescription("nb-NO")]
        nbNO,

        /// <summary>
        /// Norwegian (Bokmål)
        /// </summary>
        [EnumDescription("nb")]
        nb,

        /// <summary>
        /// Nepali
        /// </summary>
        [EnumDescription("ne")]
        ne,

        /// <summary>
        /// Nepali (Nepal)
        /// </summary>
        [EnumDescription("ne-NP")]
        neNP,

        /// <summary>
        /// Dutch
        /// </summary>
        [EnumDescription("nl")]
        nl,

        /// <summary>
        /// Dutch (Netherlands)
        /// </summary>
        [EnumDescription("nl-NL")]
        nlNL,

        /// <summary>
        /// Dutch (Belgium)
        /// </summary>
        [EnumDescription("nl-BE")]
        nlBE,

        /// <summary>
        /// Norwegian, Nynorsk (Norway)
        /// </summary>
        [EnumDescription("nn-NO")]
        nnNO,

        /// <summary>
        /// Norwegian (Nynorsk)
        /// </summary>
        [EnumDescription("nn")]
        nn,

        /// <summary>
        /// Sesotho sa Leboa
        /// </summary>
        [EnumDescription("nso")]
        nso,

        /// <summary>
        /// Sesotho sa Leboa (South Africa)
        /// </summary>
        [EnumDescription("nso-ZA")]
        nsoZA,

        /// <summary>
        /// Occitan
        /// </summary>
        [EnumDescription("oc")]
        oc,

        /// <summary>
        /// Occitan (France)
        /// </summary>
        [EnumDescription("oc-FR")]
        ocFR,

        /// <summary>
        /// Oriya
        /// </summary>
        [EnumDescription("or")]
        or,

        /// <summary>
        /// Oriya (India)
        /// </summary>
        [EnumDescription("or-IN")]
        orIN,

        /// <summary>
        /// Punjabi
        /// </summary>
        [EnumDescription("pa")]
        pa,

        /// <summary>
        /// Punjabi (India)
        /// </summary>
        [EnumDescription("pa-IN")]
        paIN,

        /// <summary>
        /// Polish
        /// </summary>
        [EnumDescription("pl")]
        pl,

        /// <summary>
        /// Polish (Poland)
        /// </summary>
        [EnumDescription("pl-PL")]
        plPL,

        /// <summary>
        /// Dari
        /// </summary>
        [EnumDescription("prs")]
        prs,

        /// <summary>
        /// Dari (Afghanistan)
        /// </summary>
        [EnumDescription("prs-AF")]
        prsAF,

        /// <summary>
        /// Pashto
        /// </summary>
        [EnumDescription("ps")]
        ps,

        /// <summary>
        /// Pashto (Afghanistan)
        /// </summary>
        [EnumDescription("ps-AF")]
        psAF,

        /// <summary>
        /// Portuguese
        /// </summary>
        [EnumDescription("pt")]
        pt,

        /// <summary>
        /// Portuguese (Brazil)
        /// </summary>
        [EnumDescription("pt-BR")]
        ptBR,

        /// <summary>
        /// Portuguese (Portugal)
        /// </summary>
        [EnumDescription("pt-PT")]
        ptPT,

        /// <summary>
        /// K'iche
        /// </summary>
        [EnumDescription("qut")]
        qut,

        /// <summary>
        /// K'iche (Guatemala)
        /// </summary>
        [EnumDescription("qut-GT")]
        qutGT,

        /// <summary>
        /// Quechua
        /// </summary>
        [EnumDescription("quz")]
        quz,

        /// <summary>        
        /// Quechua (Bolivia)
        /// </summary>
        [EnumDescription("quz-BO")]
        quzBO,

        /// <summary>
        /// Quechua (Ecuador)
        /// </summary>
        [EnumDescription("quz-EC")]
        quzEC,

        /// <summary>
        /// Quechua (Peru)
        /// </summary>
        [EnumDescription("quz-PE")]
        quzPE,

        /// <summary>
        /// Romansh
        /// </summary>
        [EnumDescription("rm")]
        rm,

        /// <summary>
        /// Romansh (Switzerland)
        /// </summary>
        [EnumDescription("rm-CH")]
        rmCH,

        /// <summary>
        /// Romanian
        /// </summary>
        [EnumDescription("ro")]
        ro,

        /// <summary>
        /// Romanian (Romania)
        /// </summary>
        [EnumDescription("ro-RO")]
        roRO,

        /// <summary>
        /// Russian
        /// </summary>
        [EnumDescription("ru")]
        ru,

        /// <summary>
        /// Russian (Russia)
        /// </summary>
        [EnumDescription("ru-RU")]
        ruRU,

        /// <summary>
        /// Kinyarwanda
        /// </summary>
        [EnumDescription("rw")]
        rw,

        /// <summary>
        /// Kinyarwanda (Rwanda)
        /// </summary>
        [EnumDescription("rw-RW")]
        rwRW,

        /// <summary>
        /// Sanskrit
        /// </summary>
        [EnumDescription("sa")]
        sa,

        /// <summary>
        /// Sanskrit (India)
        /// </summary>
        [EnumDescription("sa-IN")]
        saIN,

        /// <summary>
        /// Yakut
        /// </summary>
        [EnumDescription("sah")]
        sah,

        /// <summary>
        /// Yakut (Russia)
        /// </summary>
        [EnumDescription("sah-RU")]
        sahRU,

        /// <summary>
        /// Sami (Northern)
        /// </summary>
        [EnumDescription("se")]
        se,

        /// <summary>
        /// Sami, Northern (Norway)
        /// </summary>
        [EnumDescription("se-NO")]
        seNO,

        /// <summary>
        /// Sami, Northern (Sweden)
        /// </summary>
        [EnumDescription("se-SE")]
        seSE,

        /// <summary>
        /// Sami, Northern (Finland)
        /// </summary>
        [EnumDescription("se-FI")]
        seFI,

        /// <summary>
        /// Sinhala
        /// </summary>
        [EnumDescription("si")]
        si,

        /// <summary>
        /// Sinhala (Sri Lanka)
        /// </summary>
        [EnumDescription("si-LK")]
        siLK,

        /// <summary>
        /// Slovak
        /// </summary>
        [EnumDescription("sk")]
        sk,

        /// <summary>
        /// Slovak (Slovakia)
        /// </summary>
        [EnumDescription("sk-SK")]
        skSK,

        /// <summary>
        /// Slovenian
        /// </summary>
        [EnumDescription("sl")]
        sl,

        /// <summary>
        /// Slovenian (Slovenia)
        /// </summary>
        [EnumDescription("sl-SI")]
        slSI,

        /// <summary>
        /// Sami, Southern (Norway)
        /// </summary>
        [EnumDescription("sma-NO")]
        smaNO,

        /// <summary>
        /// Sami, Southern (Sweden)
        /// </summary>
        [EnumDescription("sma-SE")]
        smaSE,

        /// <summary>
        /// Sami (Southern)
        /// </summary>
        [EnumDescription("sma")]
        sma,

        /// <summary>
        /// Sami, Lule (Norway)
        /// </summary>
        [EnumDescription("smj-NO")]
        smjNO,

        /// <summary>
        /// Sami, Lule (Sweden)
        /// </summary>
        [EnumDescription("smj-SE")]
        smjSE,

        /// <summary>
        /// Sami (Lule)
        /// </summary>
        [EnumDescription("smj")]
        smj,

        /// <summary>
        /// Sami, Inari (Finland)
        /// </summary>
        [EnumDescription("smn-FI")]
        smnFI,

        /// <summary>
        /// Sami (Inari)
        /// </summary>
        [EnumDescription("smn")]
        smn,

        /// <summary>
        /// Sami, Skolt (Finland)
        /// </summary>
        [EnumDescription("sms-FI")]
        smsFI,

        /// <summary>
        /// Sami (Skolt)
        /// </summary>
        [EnumDescription("sms")]
        sms,

        /// <summary>
        /// Albanian
        /// </summary>
        [EnumDescription("sq")]
        sq,

        /// <summary>
        /// Albanian (Albania)
        /// </summary>
        [EnumDescription("sq-AL")]
        sqAL,

        /// <summary>
        /// Serbian (Latin, Serbia and Montenegro (Former))
        /// </summary>
        [EnumDescription("sr-Latn-CS")]
        srLatnCS,

        /// <summary>
        /// Serbian (Cyrillic, Serbia and Montenegro (Former))
        /// </summary>
        [EnumDescription("sr-Cyrl-CS")]
        srCyrlCS,

        /// <summary>
        /// Serbian (Latin, Bosnia and Herzegovina)
        /// </summary>
        [EnumDescription("sr-Latn-BA")]
        srLatnBA,

        /// <summary>
        /// Serbian (Cyrillic, Bosnia and Herzegovina)
        /// </summary>
        [EnumDescription("sr-Cyrl-BA")]
        srCyrlBA,

        /// <summary>
        /// Serbian (Latin, Serbia)
        /// </summary>
        [EnumDescription("sr-Latn-RS")]
        srLatnRS,

        /// <summary>
        /// Serbian (Cyrillic, Serbia)
        /// </summary>
        [EnumDescription("sr-Cyrl-RS")]
        srCyrlRS,

        /// <summary>
        /// Serbian (Latin, Montenegro)
        /// </summary>
        [EnumDescription("sr-Latn-ME")]
        srLatnME,

        /// <summary>
        /// Serbian (Cyrillic, Montenegro)
        /// </summary>
        [EnumDescription("sr-Cyrl-ME")]
        srCyrlME,

        /// <summary>
        /// Serbian (Cyrillic)
        /// </summary>
        [EnumDescription("sr-Cyrl")]
        srCyrl,

        /// <summary>
        /// Serbian (Latin)
        /// </summary>
        [EnumDescription("sr-Latn")]
        srLatn,

        /// <summary>
        /// Serbian
        /// </summary>
        [EnumDescription("sr")]
        sr,

        /// <summary>        
        /// Swedish
        /// </summary>
        [EnumDescription("sv")]
        sv,

        /// <summary>
        /// Swedish (Sweden)
        /// </summary>
        [EnumDescription("sv-SE")]
        svSE,

        /// <summary>
        /// Swedish (Finland)
        /// </summary>
        [EnumDescription("sv-FI")]
        svFI,

        /// <summary>
        /// Kiswahili
        /// </summary>
        [EnumDescription("sw")]
        sw,

        /// <summary>
        /// Kiswahili (Kenya)
        /// </summary>
        [EnumDescription("sw-KE")]
        swKE,

        /// <summary>
        /// Syriac
        /// </summary>
        [EnumDescription("syr")]
        syr,

        /// <summary>
        /// Syriac (Syria)
        /// </summary>
        [EnumDescription("syr-SY")]
        syrSY,

        /// <summary>
        /// Tamil
        /// </summary>
        [EnumDescription("ta")]
        ta,

        /// <summary>
        /// Tamil (India)
        /// </summary>
        [EnumDescription("ta-IN")]
        taIN,

        /// <summary>
        /// Telugu
        /// </summary>
        [EnumDescription("te")]
        te,

        /// <summary>
        /// Telugu (India)
        /// </summary>
        [EnumDescription("te-IN")]
        teIN,

        /// <summary>
        /// Tajik
        /// </summary>
        [EnumDescription("tg")]
        tg,

        /// <summary>
        /// Tajik (Cyrillic, Tajikistan)
        /// </summary>
        [EnumDescription("tg-Cyrl-TJ")]
        tgCyrlTJ,

        /// <summary>        
        /// Tajik (Cyrillic)
        /// </summary>
        [EnumDescription("tg-Cyrl")]
        tgCyrl,

        /// <summary>
        /// Thai
        /// </summary>
        [EnumDescription("th")]
        th,

        /// <summary>
        /// Thai (Thailand)
        /// </summary>
        [EnumDescription("th-TH")]
        thTH,

        /// <summary>
        /// Turkmen
        /// </summary>
        [EnumDescription("tk")]
        tk,

        /// <summary>
        /// Turkmen (Turkmenistan)
        /// </summary>
        [EnumDescription("tk-TM")]
        tkTM,

        /// <summary>
        /// Setswana
        /// </summary>
        [EnumDescription("tn")]
        tn,

        /// <summary>
        /// Setswana (South Africa)
        /// </summary>
        [EnumDescription("tn-ZA")]
        tnZA,

        /// <summary>
        /// Turkish
        /// </summary>
        [EnumDescription("tr")]
        tr,

        /// <summary>
        /// Turkish (Turkey)
        /// </summary>
        [EnumDescription("tr-TR")]
        trTR,

        /// <summary>
        /// Tatar
        /// </summary>
        [EnumDescription("tt")]
        tt,

        /// <summary>
        /// Tatar (Russia)
        /// </summary>
        [EnumDescription("tt-RU")]
        ttRU,

        /// <summary>
        /// Tamazight
        /// </summary>
        [EnumDescription("tzm")]
        tzm,

        /// <summary>
        /// Tamazight (Latin, Algeria)
        /// </summary>
        [EnumDescription("tzm-Latn-DZ")]
        tzmLatnDZ,

        /// <summary>
        /// Tamazight (Latin)
        /// </summary>
        [EnumDescription("tzm-Latn")]
        tzmLatn,

        /// <summary>
        /// Uyghur
        /// </summary>
        [EnumDescription("ug")]
        ug,

        /// <summary>
        /// Uyghur (PRC)
        /// </summary>
        [EnumDescription("ug-CN")]
        ugCN,

        /// <summary>
        /// Ukrainian
        /// </summary>
        [EnumDescription("uk")]
        uk,

        /// <summary>
        /// Ukrainian (Ukraine)
        /// </summary>
        [EnumDescription("uk-UA")]
        ukUA,

        /// <summary>
        /// Urdu
        /// </summary>
        [EnumDescription("ur")]
        ur,

        /// <summary>
        /// Urdu (Islamic Republic of Pakistan)
        /// </summary>
        [EnumDescription("ur-PK")]
        urPK,

        /// <summary>
        /// Uzbek
        /// </summary>
        [EnumDescription("uz")]
        uz,

        /// <summary>
        /// Uzbek (Latin, Uzbekistan)
        /// </summary>
        [EnumDescription("uz-Latn-UZ")]
        uzLatnUZ,

        /// <summary>
        /// Uzbek (Cyrillic, Uzbekistan)
        /// </summary>
        [EnumDescription("uz-Cyrl-UZ")]
        uzCyrlUZ,

        /// <summary>
        /// Uzbek (Cyrillic)
        /// </summary>
        [EnumDescription("uz-Cyrl")]
        uzCyrl,

        /// <summary>
        /// Uzbek (Latin)
        /// </summary>
        [EnumDescription("uz-Latn")]
        uzLatn,

        /// <summary>
        /// Vietnamese
        /// </summary>
        [EnumDescription("vi")]
        vi,

        /// <summary>
        /// Vietnamese (Vietnam)
        /// </summary>
        [EnumDescription("vi-VN")]
        viVN,

        /// <summary>
        /// Wolof
        /// </summary>
        [EnumDescription("wo")]
        wo,

        /// <summary>
        /// Wolof (Senegal)
        /// </summary>
        [EnumDescription("wo-SN")]
        woSN,

        /// <summary>
        /// isiXhosa
        /// </summary>
        [EnumDescription("xh")]
        xh,

        /// <summary>
        /// isiXhosa (South Africa)
        /// </summary>
        [EnumDescription("xh-ZA")]
        xhZA,

        /// <summary>
        /// Yoruba
        /// </summary>
        [EnumDescription("yo")]
        yo,

        /// <summary>
        /// Yoruba (Nigeria)
        /// </summary>
        [EnumDescription("yo-NG")]
        yoNG,

        /// <summary>
        /// Chinese (Simplified)
        /// </summary>
        [EnumDescription("zh-Hans")]
        zhHans,

        /// <summary>
        /// Chinese (Traditional, Taiwan)
        /// </summary>
        [EnumDescription("zh-TW")]
        zhTW,

        /// <summary>
        /// Chinese (Simplified, PRC)
        /// </summary>
        [EnumDescription("zh-CN")]
        zhCN,

        /// <summary>
        /// Chinese (Traditional, Hong Kong S.A.R.)
        /// </summary>
        [EnumDescription("zh-HK")]
        zhHK,

        /// <summary>
        /// Chinese (Simplified, Singapore)
        /// </summary>
        [EnumDescription("zh-SG")]
        zhSG,

        /// <summary>
        /// Chinese (Traditional, Macao S.A.R.)
        /// </summary>
        [EnumDescription("zh-MO")]
        zhMO,

        /// <summary>
        /// Chinese
        /// </summary>
        [EnumDescription("zh")]
        zh,

        /// <summary>
        /// Chinese (Traditional)
        /// </summary>
        [EnumDescription("zh-Hant")]
        zhHant,

        /// <summary>
        /// Chinese (Simplified) Legacy
        /// </summary>
        [EnumDescription("zh-CHS")]
        zhCHS,

        /// <summary>
        /// Chinese (Traditional) Legacy
        /// </summary>
        [EnumDescription("zh-CHT")]
        zhCHT,

        /// <summary>
        /// isiZulu
        /// </summary>
        [EnumDescription("zu")]
        zu,

        /// <summary>
        /// isiZulu (South Africa)
        /// </summary>
        [EnumDescription("zu-ZA")]
        zuZA
    }
}

