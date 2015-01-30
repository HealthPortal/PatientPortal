

//$(".pocket").tabs();
//$(".pocket-menu-sub-nav").tabs();
//$(".pocket").on("tabsactivate", function (event, ui) {
//    if (this !== event.target) {
//        return false;
//    }


//});




$('.patient-record--nav ul').each(function () {
    // For each set of tabs, we want to keep track of
    // which tab is active and it's associated content
    var $active, $content, $links = $(this).find('a');

    // If the location.hash matches one of the links, use that as the active tab.
    // If no match is found, use the first link as the initial active tab.
    $active = $($links.filter('[href="' + location.hash + '"]')[0] || $links[0]);
    $active.addClass('active');
    $content = $($active.attr('href'));

    // Hide the remaining content
    $links.not($active).each(function () {
        $($(this).attr('href')).hide();
    });

    // Bind the click event handler
    $(this).on('click', 'a', function (e) {
        // Make the old tab inactive.
        $active.removeClass('active');
        $content.hide();

        // Update the variables with the new link and content
        $active = $(this);
        $content = $($(this).attr('href'));

        // Make the tab active.
        $active.addClass('active');
        $content.show();

        // Prevent the anchor's default click action
        e.preventDefault();
    });
});








//$(".nav-toggle").toggle(function () {
//    $(".full-wrap").addClass("closed");
//}, function () {
//    $(".full-wrap").removeClass("closed");
//});



//$(".pocket-menu-sub-nav li").on("click", function (e) {
//    e.preventDefault();
//    var list = $(this).find(".pocket-menu-sub-nav a").attr("href").replace("#", "");
//    $(".pocket-sub.active,.pocket-menu-sub-nav li").removeClass("active");
//    $(".pocket-sub--" + list).addClass("active");
//    $(this).addClass("active");
//});


