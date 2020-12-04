/*** 
    Simple jQuery Slideshow Script
    Released by Jon Raasch (jonraasch.com) under FreeBSD license: free to use or modify, not responsible for anything, etc.  Please link out to me if you like it :)
***/

function slideSwitch() {
    var $active = $('#slideshow li.active');

    if ( $active.length == 0 ) $active = $('#slideshow li:last');

    // use this to pull the divs in the order they appear in the markup
    var $next =  $active.next().length ? $active.next()
        : $('#slideshow li:first');

    // uncomment below to pull the divs randomly
    // var $sibs  = $active.siblings();
    // var rndNum = Math.floor(Math.random() * $sibs.length );
    // var $next  = $( $sibs[ rndNum ] );


    $active.addClass('last-active');

    $next.css({opacity: 0.0})
        .addClass('active')
        .animate({opacity: 1.0}, 1700, function() {
            $active.removeClass('active last-active');
        });
}

$(function() {
    setInterval( "slideSwitch()", 5000 );
});

/*** end slideshow ***/

/*** jquote script ***/

(function($){
 $.fn.jquote = function(options) {

  var defaults = {
   bquoteclass: "jquote",
   srcclass: "source"
   };
   
  var options = $.extend(defaults, options);
    
  return this.each(function() {
        //the magic. Remember access options by using options.optionname!
        //jQuote - originally written by Jack Franklin December 2008.
        //Rewritten to correct small bugs and better, cleaner code Feb 2009. 2 months late!
        //RELEASED TO THE WORLD = 14th Feb 2009.
        //Visit jackfranklin.co.uk/jquote for help.
        //Follow @jQuote on Twitter - twitter.com/jquote.
        var opentag = "<blockquote class=\"" + options.bquoteclass + "\">";
        var thequote = $("span.quote").text();
        var end = "</blockquote>";
        var source1 = "<span class=\"" + options.srcclass + "\">";
        var source2 = "</span>";
        var person = $("span.quote").attr("title");
        $("span.quote").after(opentag+thequote+source1+person+source2+end);
        
        
  });
 };
})(jQuery);

$("p").jquote({
                    bquoteclass: "jquote", //The class of the blockquote element.
                    srcclass: "source" //The class of the span element containing the source.
                });
/*** jquote script ***/
