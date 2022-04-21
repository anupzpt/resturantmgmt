function getCalendarDivString(a) {
	var b = '<div id="ndp-nepali-box" class="ndp-corner-all" style="display:none">';
	return b += '<span id="ndp-target-id" style="display:none"></span>', b += '<div class="ndp-corner-all ndp-header">', b += '<a href="javascript:void(0)" id="prev" title="Previous Month" class="ndp-prev"></a>', b += '<a href="javascript:void(0)" id="next" title="Next Month" class="ndp-next"></a>', b += '<span id="currentMonth"></span>', b += "</div>", b += '<div id="npd-table-div">', b += "<table>", b += '<tr class="ndp-days">', b += "<th>\u0906</th>", b += "<th>\u0938\u094b</th>", b += "<th>\u092e\u0902</th>", b += "<th>\u092c\u0941</th>", b += "<th>\u092c\u093f</th>", b += "<th>\u0936\u0941</th>", b += "<th>\u0936</th>", b += "</tr>", b += "</table>", b += "</div>", b += "</div>"
}
function showNdpCalendarBox(a) {
    console.log(a);
	if (npdCalendarVisible) hideCalendarBox(!1);
	else {
		ndpData[a] && (ndpAttr = ndpData[a]);
		var b = $("#" + a).val();
		$("#ndp-target-id").html(a);
		var c = $("#" + a).offset();
        $("#ndp-nepali-box").css("top", c.top + $("#" + a).outerHeight()), $("#ndp-nepali-box").css("left", c.left), showCalendar(b), npdCalendarVisible = !0
	}
}
function setSelectedDay(a) {
	var b = $("#ndp-target-id").html();
	console.log("print ", a);
	console.log($("#" + b).data('ndpenglishinput'));
	$("#" + b).val(a), ndpAttr.ndpEnglishInput && $("#" + ndpAttr.ndpEnglishInput).val(BS2AD(a)), hideCalendarBox();
	$("#" + b).data('ndpenglishinput') && $( $("#" + b).data('ndpenglishinput') ).val(BS2AD(a));
}
function showCalendar(a) {
	$("#ndp-nepali-box table").find("tr:gt(0)").remove(), "" === a ? $("#ndp-nepali-box table").append(getDateTable("")) : $("#ndp-nepali-box table").append(getDateTable(a)), "block" == $("#ndp-nepali-box").css("display") && $("#ndp-nepali-box").hide(), $("#ndp-nepali-box").fadeIn(100)
}
function getDateTable(a) {
	var b = "",
		c = "";
	if ("" === a) {
		var d = getNepaliDate();
		return b = getMonthParameters(d), c = getDateRows(b[0], b[1], b[2], b[3], b[4])
	}
	return b = getMonthParameters(a), c = getDateRows(b[0], b[1], b[2], b[3], b[4])
}
function performSelectMonth(a) {
	showCalendar((ndpAttr.npdYear ? $("#npd-year-select").val() : a) + "-" + $("#npd-month-select").val() + "-01")
}
function performSelectYear(a) {
	showCalendar($("#npd-year-select").val() + "-" + (ndpAttr.npdMonth ? $("#npd-month-select").val() : a) + "-01")
}
function getMonthSelect(a, b) {
	var c = getNepaliMonthsInNepali(),
		d = '<select id="npd-month-select" onchange="performSelectMonth(' + b + ')">';
	return c.forEach(function(b, c) {
		d += '<option value="' + (c + 1) + '"' + (c + 1 == a ? " selected" : "") + ">" + b + "<li>"
	}), d += "<select>"
}
function getYearSelect(a, b, c) {
	console.log(c);
	var d = '<select id="npd-year-select" onchange="performSelectYear(' + a + ')">';
	for (i = Math.round(c / 2) > 0 && parseInt(b) - Math.round(c / 2) >= 2e3 ? parseInt(b) - Math.round(c / 2) : 2e3; i <= (Math.round(c / 2) > 0 && parseInt(b) + Math.round(c / 2) <= 2090 ? parseInt(b) + Math.round(c / 2) : 2090); i++) d += '<option value="' + i + '"' + (b == i ? " selected" : "") + ">" + englishNo2Nep(i) + "<li>";
	return d += "<select>"
}
function getMonthParameters(a) {
	var b = a.split("-"),
		c = b[0],
		d = b[1],
		e = ndpAttr.npdYearCount || 0;
	ndpAttr.npdMonth && ndpAttr.npdYear ? $("#currentMonth").html(getMonthSelect(d, c) + getYearSelect(d, c, e)) : ndpAttr.npdMonth ? $("#currentMonth").html(getMonthSelect(d, c) + " " + englishNo2Nep(c)) : ndpAttr.npdYear ? $("#currentMonth").html(getNepaliMonth(d - 1) + " " + getYearSelect(d, c, e)) : $("#currentMonth").html(getNepaliMonth(d - 1) + "&nbsp;" + englishNo2Nep(c)), nYear = pYear = c, nMonth = parseInt(d, 10) + 1, nMonth > 12 && (nYear++, nMonth = "01"), pMonth = parseInt(d, 10) - 1, pMonth < 1 && (pYear--, pMonth = "12"), $("#prev").attr("onclick", "showCalendar('" + pYear + "-" + pMonth + "-01')"), $("#next").attr("onclick", "showCalendar('" + nYear + "-" + nMonth + "-01')");
	var f = b[2],
		g = numberOfBsDays(c, d - 1),
		h = new NepaliDateConverter,
		i = d + "/1/" + c,
		j = h.bs2ad(i),
		k = j.split("-"),
		l = k[0],
		m = k[1],
		n = k[2],
		o = new Date(l, m - 1, n),
		p = o.getDay();
	return new Array(p, g, c, d, f)
}
function getDateRows(a, b, c, d, e) {
	for (var f = getNepaliDate(), g = f.split("-"), h = g[0], i = get2DigitNo(g[1]), j = get2DigitNo(g[2]), k = "", l = 0; a + b > l; l++) {
		l % 7 === 0 && (k += "<tr>");
		var m = l - a + 1,
			n = c.toString() + "-" + get2DigitNo(d) + "-" + get2DigitNo(m),
			o = "";
		o = c == h && get2DigitNo(d) == i && j == get2DigitNo(m) ? "ndp-selected" : m == e ? "ndp-current" : "ndp-date", a > l ? k += "<td></td>\n" : (k += "<td class='" + o + "'>", k += "<a href='javascript:void(0)' onclick=\"setSelectedDay('" + n + "')\">" + englishNo2Nep(m) + "</a>", k += "</td>\n"), l % 7 == 6 && (k += "</tr>\n")
	}
	return k
}
function hideCalendarBox(a) {
	a = "undefined" != typeof a ? a : !0, $("#ndp-nepali-box").fadeOut(100), npdCalendarVisible = !1, a && ndpAttr.onChange && ndpAttr.onChange()
}
function get2DigitNo(a) {
	return a = parseInt(a, 10), 10 > a ? "0" + a.toString() : a.toString()
}
function getMonths() {
	var a = new Array("January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December");
	return a
}
function getNepaliMonths() {
	var a = new Array("Baisakh", "Jestha", "Ashar", "Shrawan", "Bhadra", "Ashoj", "Kartik", "Mangsir", "Poush", "Magh", "Falgun", "Chaitra");
	return a
}
function getNepaliDaysShort() {
	var a = new Array("\u0906", "\u0938\u094b", "\u092e\u0902", "\u092c\u0941", "\u092c\u093f", "\u0936\u0941", "\u0936");
	return a
}
function getNepaliMonthsInNepali() {
	return new Array("\u092c\u0948\u0936\u093e\u0916", "\u091c\u0947\u0920", "\u0905\u0937\u093e\u0922", "\u0936\u094d\u0930\u093e\u0935\u0923", "\u092d\u093e\u0926\u094d\u0930", "\u0906\u0936\u094d\u0935\u093f\u0928", "\u0915\u093e\u0930\u094d\u0924\u093f\u0915", "\u092e\u0919\u094d\u0938\u093f\u0930", "\u092a\u094c\u0937", "\u092e\u093e\u0918", "\u092b\u093e\u0932\u094d\u0917\u0941\u0928", "\u091a\u0948\u0924\u094d\u0930")
}
function getNepaliMonth(a) {
	a = parseInt(a, 10);
	var b = getNepaliMonthsInNepali();
	return b[a]
}
function getCurrentDayName() {
	var a = new Date,
		b = a.getDay(),
		c = new Array(7);
	return c[0] = "Sunday", c[1] = "Monday", c[2] = "Tuesday", c[3] = "Wednesday", c[4] = "Thursday", c[5] = "Friday", c[6] = "Saturday", c[b]
}
function getDayFromDate(a) {
	var b = a.split("-"),
		c = b[2],
		d = b[1],
		e = b[0],
		f = new Date(e, d - 1, c),
		g = f.getDay(),
		h = new Array("Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat");
	return h[g]
}
function numberOfBsDays(a, b) {
	var c = new NepaliDateConverter;
	return c.bs[a][b]
}
function numberOfDays(a, b) {
	var c = new Date(a, b, 0);
	return c.getDate()
}
function AD2BS(a) {
	var b = new NepaliDateConverter;
	return b.ad2bs(getNepaliFormat(a))
}
function BS2AD(a) {
	var b = new NepaliDateConverter;
	return b.bs2ad(getNepaliFormat(a))
}
function getNepaliDate() {
	var a = new NepaliDateConverter;
	return a.ad2bs(getDateInNo("/"))
}
function getDateInWords() {
	var a = getMonths(),
		b = new Date,
		c = b.getDate(),
		d = b.getMonth() + 1,
		e = b.getYear(),
		f = 1e3 > e ? e + 1900 : e;
	return c + " " + a[d] + ", " + f
}
function getDateInNo(a) {
	var b = new Date,
		c = b.getDate(),
		d = b.getMonth() + 1,
		e = b.getFullYear();
	return d + a + c + a + e
}
function getNepaliFormat(a) {
	var b = a.split("-"),
		c = b[2],
		d = b[1],
		e = b[0];
	return d + "/" + c + "/" + e
}
function getAdDateInWords(a) {
	var b = getMonths(),
		c = a.split("-"),
		d = c[2],
		e = c[1],
		f = c[0];
	return d + " " + b[e] + ", " + f
}
function getNepaliDateInWords(a) {
	var b = getNepaliMonths(),
		c = a.split("-"),
		d = c[2],
		e = c[1],
		f = c[0];
	return d + " " + b[e] + ", " + f
}
function getCurrentYear() {
	var a = new Date,
		b = a.getFullYear();
	return b
}
function getCurrentMonth() {
	var a = new Date,
		b = a.getMonth() + 1;
	return b
}
function getCurrentDay() {
	var a = new Date,
		b = a.getDate();
	return b
}
function makeArray() {
	for (i = 0; i < makeArray.arguments.length; i++) this[i + 1] = makeArray.arguments[i]
}
function englishNo2Nep(a) {
	a = a.toString();
	for (var b = "", c = 0; c < a.length; c++) b += convertNos(a[c]);
	return b
}
function convertNos(a) {
	switch (a) {
	case "\u0966":
		return 0;
	case "\u0967":
		return 1;
	case "\u0968":
		return 2;
	case "\u0969":
		return 3;
	case "\u096a":
		return 4;
	case "\u096b":
		return 5;
	case "\u096c":
		return 6;
	case "\u096d":
		return 7;
	case "\u096e":
		return 8;
	case "\u096f":
		return 9;
	case "0":
		return "\u0966";
	case "1":
		return "\u0967";
	case "2":
		return "\u0968";
	case "3":
		return "\u0969";
	case "4":
		return "\u096a";
	case "5":
		return "\u096b";
	case "6":
		return "\u096c";
	case "7":
		return "\u096d";
	case "8":
		return "\u096e";
	case "9":
		return "\u096f"
	}
}
function NepaliDateConverter() {
    //console.log('check point');
	this.bs_date_eq = "09/17/2000", this.ad_date_eq = "01/01/1944", this.bs = [], this.bs[2e3] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2001] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2002] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2003] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2004] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2005] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2006] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2007] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2008] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31), this.bs[2009] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2010] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2011] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2012] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2013] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2014] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2015] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2016] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2017] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2018] = new Array(31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2019] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2020] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2021] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2022] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2023] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2024] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2025] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2026] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2027] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2028] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2029] = new Array(31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30), this.bs[2030] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2031] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2032] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2033] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2034] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2035] = new Array(30, 32, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31), this.bs[2036] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2037] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2038] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2039] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2040] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2041] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2042] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2043] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2044] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2045] = new Array(31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2046] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2047] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2048] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2049] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2050] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2051] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2052] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2053] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2054] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2055] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2056] = new Array(31, 31, 32, 31, 32, 30, 30, 29, 30, 29, 30, 30), this.bs[2057] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2058] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2059] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2060] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2061] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2062] = new Array(30, 32, 31, 32, 31, 31, 29, 30, 29, 30, 29, 31), this.bs[2063] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2064] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2065] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2066] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 29, 31), this.bs[2067] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2068] = new Array(31, 31, 32, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2069] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2070] = new Array(31, 31, 31, 32, 31, 31, 29, 30, 30, 29, 30, 30), this.bs[2071] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2072] = new Array(31, 32, 31, 32, 31, 30, 30, 29, 30, 29, 30, 30), this.bs[2073] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 31), this.bs[2074] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2075] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2076] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2077] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 30, 29, 31), this.bs[2078] = new Array(31, 31, 31, 32, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2079] = new Array(31, 31, 32, 31, 31, 31, 30, 29, 30, 29, 30, 30), this.bs[2080] = new Array(31, 32, 31, 32, 31, 30, 30, 30, 29, 29, 30, 30), this.bs[2081] = new Array(31, 31, 32, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2082] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2083] = new Array(31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2084] = new Array(31, 31, 32, 31, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2085] = new Array(31, 32, 31, 32, 30, 31, 30, 30, 29, 30, 30, 30), this.bs[2086] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2087] = new Array(31, 31, 32, 31, 31, 31, 30, 30, 29, 30, 30, 30), this.bs[2088] = new Array(30, 31, 32, 32, 30, 31, 30, 30, 29, 30, 30, 30), this.bs[2089] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.bs[2090] = new Array(30, 32, 31, 32, 31, 30, 30, 30, 29, 30, 30, 30), this.count_ad_days = count_ad_days, this.count_bs_days = count_bs_days, this.add_bs_days = add_bs_days, this.add_ad_days = add_ad_days, this.bs2ad = bs2ad, this.ad2bs = ad2bs
}
function count_ad_days(a, b) {
	var c = 864e5,
		d = a.split("/"),
		e = b.split("/");
	d[2] = Number(d[2]), d[1] = Number(d[1]), d[0] = Number(d[0]), e[2] = Number(e[2]), e[1] = Number(e[1]), e[0] = Number(e[0]);
	var f = new Date(d[2], d[0] - 1, d[1]),
		g = new Date(e[2], e[0] - 1, e[1]),
		h = Math.ceil((g.getTime() - f.getTime()) / c);
	return h
}
function count_bs_days(a, b) {
	var c = a.split("/"),
		d = b.split("/"),
		e = Number(c[2]),
		f = Number(c[0]),
		g = Number(c[1]),
		h = Number(d[2]),
		i = Number(d[0]),
		j = Number(d[1]),
		k = 0,
		l = 0;
	for (l = e; h >= l; l++) k += arraySum(this.bs[l]);
	for (l = 0; f > l; l++) k -= this.bs[e][l];
	for (k += this.bs[e][11], l = i - 1; 12 > l; l++) k -= this.bs[h][l];
	return k -= g + 1, k += j - 1
}
function add_ad_days(a, b) {
	var c = new Date(a);
	return c.setDate(c.getDate() + b), ad_month = c.getMonth() + 1, ad_day = c.getDate(), c.getFullYear() + "-" + (ad_month < 10 ? "0" + ad_month.toString() : ad_month) + "-" + (ad_day < 10 ? "0" + ad_day.toString() : ad_day)
}
function add_bs_days(a, b) {
	var c = a.split("/"),
		d = Number(c[2]),
		e = Number(c[0]),
		f = Number(c[1]);
	for (f += b; f > this.bs[d][e - 1];) f -= this.bs[d][e - 1], e++, e > 12 && (e = 1, d++);
	return d + "-" + (10 > e ? "0" + e.toString() : e) + "-" + (10 > f ? "0" + f.toString() : f)
}
function bs2ad(a) {
	return days_count = this.count_bs_days(this.bs_date_eq, a), this.add_ad_days(this.ad_date_eq, days_count)
}
function ad2bs(a) {
	return days_count = this.count_ad_days(this.ad_date_eq, a), this.add_bs_days(this.bs_date_eq, days_count)
}
ndpAttr = {}, npdCalendarVisible = "", npdIgnoreMouseUp = !1, ndpData = [], function(a) {
    a.fn.nepaliDatePicker = function (b) {
      //  console.log('chk 1');
        b = "undefined" != typeof b ? b : {}, ndpAttr = b, this.each(function () {
            console.log('chk2 ');
			var c = a(this).attr("id");
			a(this).addClass("ndp-nepali-calendar"), ndpData[c] = b, ndpAttr.onFocus !== !1 && a(this).attr("onfocus", "showNdpCalendarBox('" + c + "')"), ndpAttr.ndpTriggerButton && a(this).after('<button type="button" class="ndp-click-trigger ' + (ndpAttr.ndpTriggerButtonClass ? ndpAttr.ndpTriggerButtonClass : "") + '" onclick="showNdpCalendarBox(&quot;' + c + '&quot;)">' + (ndpAttr.ndpTriggerButtonText ? ndpAttr.ndpTriggerButtonText : "Pick Date") + "</button>")
		}), a("body").append(getCalendarDivString(ndpAttr)), a(".ndp-nepali-calendar, #ndp-nepali-box").hover(function() {
			mouse_is_inside = !0
		}, function() {
			mouse_is_inside = !1
		}), a("html").mouseup(function(b) {
			a(b.target).is(".ndp-click-trigger") || npdCalendarVisible && !mouse_is_inside && hideCalendarBox(!1)
		})
	}
}(jQuery);
var mouse_is_inside = !1;
arraySum = function(a) {
	for (var b = 0, c = a.length; c; b += a[--c]);
	return b
};
