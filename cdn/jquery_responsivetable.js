From: "Saved by Internet Explorer 11"
Subject: 
Date: Tue, 17 May 2016 11:32:25 +0530
MIME-Version: 1.0
Content-Type: text/html;
	charset="Windows-1252"
Content-Transfer-Encoding: quoted-printable
Content-Location: http://declanbright.awardspace.info/downloads/jquery.responsivetable.js
X-MimeOLE: Produced By Microsoft MimeOLE V10.0.10011.16384

<!DOCTYPE HTML>
<!DOCTYPE html PUBLIC "" ""><HTML><HEAD><META content=3D"IE=3D11.0000"=20
http-equiv=3D"X-UA-Compatible">

<META http-equiv=3D"Content-Type" content=3D"text/html; =
charset=3Dwindows-1252">
<META name=3D"GENERATOR" content=3D"MSHTML 11.00.10586.306"></HEAD>
<BODY>
<PRE>(function ($) {=0A=
    var defaults, w,=0A=
		_responsiveTable =3D 'responsiveTable',=0A=
		_container =3D 'Container',=0A=
		_overflow =3D 'overflow',=0A=
		_static =3D 'Static',=0A=
		_overflowContainer =3D _overflow + _container,=0A=
		_staticContainer =3D 'static' + _container,=0A=
		_nthChild =3D 'nth-child',=0A=
		_divClass =3D '&lt;div class=3D"',=0A=
		isResponsive =3D false,=0A=
		rPaths =3D ["thead", "", "tbody", "tfoot"],=0A=
		rRoot,=0A=
		rRows,=0A=
        responsiveContainers,=0A=
		tableWidth,=0A=
		tableStatic, tableStaticRow, rowCells, rowCellsClone, uiHint, trim;=0A=
=0A=
    $.fn.responsiveTable =3D function (options) {=0A=
        w =3D $(window);=0A=
        defaults =3D {=0A=
            staticColumns: 1,=0A=
            scrollRight: true,=0A=
            scrollHintEnabled: true,=0A=
            scrollHintDuration: 2000=0A=
        };=0A=
        options =3D $.extend(defaults, options);=0A=
=0A=
        function setupScrollHint(table, offset) {=0A=
            // Display a hint to the user that the table contents are =
scrollable=0A=
            var uid =3D _responsiveTable + 'UiHint';=0A=
            if ($('#' + uid).length =3D=3D 0) {=0A=
                uiHint =3D $('&lt;div id=3D"' + uid + =
'"&gt;&amp;lt;&amp;lt;  Scroll table left and right  =
&amp;gt;&amp;gt;&lt;/div&gt;');=0A=
                $('body').prepend(uiHint);=0A=
                var ht =3D w.height() / 2;=0A=
                if (offset.top &gt; 0 &amp;&amp; table.height() &gt; 0) {=0A=
                    ht =3D offset.top + (table.height() * 0.4);=0A=
                }=0A=
                uiHint.css({=0A=
                    "position": "absolute",=0A=
                    "z-index": 1000000,=0A=
                    "padding": "0.5em",=0A=
                    "background-color": "#888",=0A=
                    "color": "#eee",=0A=
                    "font-size": "1.1em",=0A=
                    "border-radius": "0.6em"=0A=
                }).css({=0A=
                    "top": ht,=0A=
                    "left": (w.width() / 2) - (uiHint.width() / 2)=0A=
                });=0A=
            }=0A=
            setTimeout('$("#' + uid + '").hide();', =
options.scrollHintDuration);=0A=
        }=0A=
=0A=
        function getResponsiveContainers(table) {=0A=
            var oc =3D table.parent();=0A=
            return { rtc: oc.parent(), oc: oc, sc: oc.parent().find('.' =
+ _staticContainer) };=0A=
        }=0A=
=0A=
        function resizeResponsiveTable(table, trim) {=0A=
            // Resize the overflow container=0A=
            responsiveContainers =3D getResponsiveContainers(table);=0A=
            // Set the overflow container width=0A=
            =
responsiveContainers.oc.width(responsiveContainers.rtc.innerWidth() - =
responsiveContainers.sc.outerWidth() - trim);=0A=
            // Scroll right so the right hand side is displayed by =
default=0A=
            if (options.scrollRight) {=0A=
                responsiveContainers.oc.scrollLeft(table.width());=0A=
            }=0A=
        }=0A=
=0A=
        function setResponsiveTable(table) {=0A=
            if (!table.parent().hasClass(_overflowContainer)) {=0A=
                var tos =3D table.offset();=0A=
                table.wrap(_divClass + _responsiveTable + _container + =
'" style=3D"' + _overflow + ':hidden;"/&gt;');=0A=
                tableStatic =3D $('&lt;table/&gt;');=0A=
=0A=
                // Copy table attributes=0A=
                $.each(table[0].attributes, function (index, a) {=0A=
                    if (a.name !=3D=3D 'id') {=0A=
                        tableStatic.attr(a.name, a.value);=0A=
                    }=0A=
                });=0A=
                tableStatic.addClass(_responsiveTable + _static)=0A=
					.css('border-right', 'ridge')=0A=
					.width(0);=0A=
				// For each row path move the rows to the same row path in the =
static table=0A=
				// Handle cases where thead, tbody &amp; tfoot are used, there may =
also be multiple tbody tags=0A=
				$.each(rPaths, function(i, rp) {=0A=
					table.find(rp).each(function(j, rs) {=0A=
						rRows =3D $(rs).find('&gt;tr');=0A=
						if (rRows.length &gt; 0) {=0A=
							rRoot =3D tableStatic;=0A=
							if (rp !=3D "") {=0A=
								rRoot =3D $('&lt;' + rp + '/&gt;');=0A=
							}=0A=
							$.each(rRows, function (k, r) {=0A=
								tableStaticRow =3D $('&lt;tr/&gt;');=0A=
								// Set the height to the calculated height of the original row, =
the natural height of the cloned cells may be different=0A=
								tableStaticRow.outerHeight($(r).outerHeight());=0A=
								// Copy row attributes=0A=
								$.each(r.attributes, function (index, a) {=0A=
									if (a.name !=3D=3D 'id') {=0A=
										tableStaticRow.attr(a.name, a.value);=0A=
									}=0A=
								});=0A=
								// Move the cells for the configured number of columns to the =
static table=0A=
								rowCells =3D $(r).find('&gt;th:' + _nthChild + '(-n+' + =
options.staticColumns + '),&gt;td:' + _nthChild + '(-n+' + =
options.staticColumns  + ')');=0A=
								rowCells.appendTo(tableStaticRow);=0A=
								$(rRoot).append(tableStaticRow);=0A=
							});=0A=
							if(rp !=3D "") {=0A=
								tableStatic.append(rRoot);=0A=
							}=0A=
						}=0A=
					});=0A=
				});=0A=
                table.before(tableStatic);=0A=
=0A=
                table.wrap(_divClass + _overflowContainer + '" =
style=3D"float:left;' + _overflow + ':scroll;' + _overflow + =
'-y:hidden;"/&gt;');=0A=
                tableStatic.wrap(_divClass + _staticContainer + '" =
style=3D"float:left;"/&gt;');=0A=
=0A=
                if (options.scrollHintEnabled) {=0A=
                    setupScrollHint(table, tos);=0A=
                }=0A=
            }=0A=
=0A=
            resizeResponsiveTable(table, 0);=0A=
=0A=
            // Check the positions and resize while alignment is not =
correct=0A=
            responsiveContainers =3D getResponsiveContainers(table);=0A=
			trim =3D 0;=0A=
            while (responsiveContainers.sc.position().top &lt; =
responsiveContainers.oc.position().top &amp;&amp; =
responsiveContainers.oc.width() &gt; 0) {=0A=
				trim++;=0A=
                resizeResponsiveTable(table, trim);=0A=
            }=0A=
        }=0A=
=0A=
        function unsetResponsiveTable(table) {=0A=
            if (table.parent().hasClass(_overflowContainer)) {=0A=
				responsiveContainers =3D getResponsiveContainers(table);=0A=
				tableStatic =3D responsiveContainers.sc.find('.' + _responsiveTable =
+ _static);=0A=
				// For each row path move the rows to the same row path in the =
original table=0A=
				$.each(rPaths, function(i, rp){=0A=
					tableStatic.find(rp).each(function(j, rs) {=0A=
						rRows =3D $(rs).find('&gt;tr');=0A=
						if (rRows.length &gt; 0) {=0A=
							rRoot =3D table;=0A=
							if (rp !=3D "") {=0A=
								rRoot =3D table.find(rp + ':eq' + '(' + j + ')');=0A=
							}=0A=
							$.each(rRows, function (k, r) {=0A=
								var nc =3D k + 1;=0A=
								rowCells =3D $(r).find('&gt;th,&gt;td');=0A=
								rowCells.prependTo(rRoot.find('&gt;tr:' + _nthChild + '(' + nc + =
')'));=0A=
							});=0A=
						}=0A=
					});=0A=
				});=0A=
                responsiveContainers.sc.remove();=0A=
                table.unwrap().unwrap();=0A=
            }=0A=
        }=0A=
=0A=
        // Detect overflow by checking the table width against that of =
its parent tree=0A=
        function setupResponsiveTable(table) {=0A=
            tableWidth =3D table.width();=0A=
            if (table.parent().hasClass(_overflowContainer)) {=0A=
                tableWidth +=3D $('.' + _staticContainer).width();=0A=
            }=0A=
=0A=
            isResponsive =3D false;=0A=
            table.parents().each(function () {=0A=
                if (!$(this).hasClass(_overflowContainer)=0A=
					&amp;&amp; tableWidth &gt; $(this).width()=0A=
				) {=0A=
                    isResponsive =3D true;=0A=
=0A=
                    // break out of each=0A=
                    return (false);=0A=
                }=0A=
            });=0A=
=0A=
            // Set or unset the responsive table=0A=
            if (isResponsive) {=0A=
                setResponsiveTable(table);=0A=
            } else {=0A=
                unsetResponsiveTable(table);=0A=
            }=0A=
        }=0A=
=0A=
        return this.each(function () {=0A=
            var $this =3D $(this);=0A=
            if (!$this.hasClass(_responsiveTable + _static)) {=0A=
                w.on('resize orientationchange', function () {=0A=
                    setupResponsiveTable($this);=0A=
                });=0A=
                setupResponsiveTable($this);=0A=
            }=0A=
        });=0A=
    };=0A=
})(jQuery); =0A=
=0A=
</PRE></BODY></HTML>
