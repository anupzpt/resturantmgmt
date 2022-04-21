/*
* For Nepali Amount to Word Conversion
* Sajan Maharjan
* sajanmaharjan.com.np
* sajanm@live.com
* 
* function -> translate_nepali_num_into_words(number)
* input -> number
* return -> String
*/
function getBelowHundred(t) { return teens[t] } function translate_nepali_num_into_words(t) { if (isNaN(t) || "" == t) return "N/A"; var n = "", e = 0; if (-1 != t.indexOf(".") && (number_parts = t.split("."), t = number_parts[0], e = number_parts[1], decimal_tens = e.substring(0, 2).toString(), 1 == decimal_tens.length && (decimal_tens = decimal_tens.toString() + "0"), decimal_words = teens[parseInt(decimal_tens)].toString() + " पैसा"), t.length > 13) return void alert("Number greater than kharab not supported"); var r = Math.floor(t % 100); if (t.toString().length > 2) var s = t.toString().substring(t.toString().length - 3, t.toString().length - 2); var i = Math.floor(t % 1e5); i = i.toString(), i = i.substring(0, i.length - 3); var a = Math.floor(t % 1e7); a = a.toString(), a = a.substring(0, a.length - 5); var o = Math.floor(t % 1e9); o = o.toString(), o = o.substring(0, o.length - 7); var g = Math.floor(t % 1e11); g = g.toString(), g = g.substring(0, g.length - 9); var l = Math.floor(t % 1e13); return l = l.toString(), l = l.substring(0, l.length - 11), l > 0 && (n += teens[l] + " खरब"), g > 0 && (n += " " + teens[g] + " अरब"), o > 0 && (n += " " + teens[o] + " करोड"), a > 0 && (n += " " + teens[a] + " लाख"), i > 0 && (n += " " + teens[i] + " हजार"), s > 0 && (n += " " + units[s] + " सय"), r > 0 && (n += " " + teens[r]), n += " रुपैंया", e > 0 && (n += ", " + decimal_words), n } var units = ["सुन्य", "एक", "दुई", "तीन", "चार", "पाँच", "छ", "सात", "आठ", "नौ", "दस"], teens = ["सुन्य", "एक", "दुई", "तीन", "चार", "पाँच", "छ", "सात", "आठ", "नौ", "दस", "एघार", "बाह्र", "तेह्र", "चौध", "पन्ध्र", "सोह्र", "सत्र", "अठाह्र", "उन्नाइस", "बीस", "एकाइस", "बाइस", "तेइस", "चौबीस", "पचीस", "छब्बीस", "सत्ताइस", "अठ्ठाइस", "उनन्तीस", "तीस", "एकतीस", "बत्तीस", "तेत्तिस", "चौतीस", "पैतीस", "छत्तिस", "सठ्तिस", "अठ्तीस", "उनन्चालीस", "चालीस", "एकचालीस", "बयालिस", "त्रिचालीस", "चौवालिस", "पैंतालिस", "छयालिस", "सतचालिस", "अठचालिस", "उनन्चास", "पचास", "एकाउन्न", "बाउन्न", "त्रिपन्न", "चौवन्न", "पच्पन्न", "छपन्न", "सन्ताउन्न", "अन्ठाउँन्न", "उनान्न्साठी", "साठी", "एकसाठी", "बासाठी", "त्रिसाठी", "चौंसाठी", "पैसाठी", "छैसठी", "सत्सठ्ठी", "अर्सठ्ठी", "उनन्सत्तरी", "सत्तरी", "एकहत्तर", "बहत्तर", "त्रिहत्तर", "चौहत्तर", "पचहत्तर", "छयहत्तर", "सत्हत्तर", "अठ्हत्तर", "उनास्सी", "अस्सी", "एकासी", "बयासी", "त्रीयासी", "चौरासी", "पचासी", "छयासी", "सतासी", "अठासी", "उनान्नब्बे", "नब्बे", "एकान्नब्बे", "बयान्नब्बे", "त्रियान्नब्बे", "चौरान्नब्बे", "पंचान्नब्बे", "छयान्नब्बे", "सन्तान्‍नब्बे", "अन्ठान्नब्बे", "उनान्सय"], tens = ["", "दस", "बीस", "तीस", "चालीस", "पचास", "साठी", "सतरी", "अस्सी", "नब्बे"];
/*
* End
* For Nepali Amount to Word Conversion
* Sajan Maharjan
*/
