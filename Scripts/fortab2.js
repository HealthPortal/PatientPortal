





$(".nav-toggle").toggle(function () {
    $(".full-wrap").addClass("closed");
}, function () {
    $(".full-wrap").removeClass("closed");
});




$('.pocket-menu-nav ul').each(function () {
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


$('#subnav li').each(function () {
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

//$("#subnav li").on("click", function (e) {
//    e.preventDefault();
//    var list = $(this).find("#subnav  a").attr("href").replace("#", "");
//    $(".pocket-sub.active,#subnav  li").removeClass("active");
//    $(".pocket-sub--" + list).addClass("active");
//    $(this).addClass("active");
//});





//$('.pocket-menu-nav .tabs [data-toggle="collapse"]').on('click', function () {

//    if ($($(this).attr('href')).hasClass('in')) return false;

//    //make collapse links act like tabs
//    $(this).parent().addClass('active').siblings('li').removeClass('active');
//    //Activate first sub tab
//    $($(this).attr('href')).find('[data-toggle="tab"]').first().tab('show');

//});

//$('.pocket-menu-nav .tabs > li > a').on('click', function () {
//    if ($(this).data('toggle') != 'collapse') {
//        $(this).closest('.pocket-menu-nav .tabs').find('.collapse.in').collapse('hide').find('.active').removeClass('active');

//    }

//});