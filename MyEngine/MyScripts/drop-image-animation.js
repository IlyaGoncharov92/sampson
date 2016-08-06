function mouseOver(e) {
    jQuery(e).children().css({
        "position": "relative",
        "z-index": "999",
        "transition": ".5s"
    })
    jQuery(e).find('.drop_container').css({
        "visibility": "visible",
        "position": "absolute",
        "z-index": "998",
        "width": "308px",
        "border": "1px solid #e3e3e3",
        "box-shadow": "0 0 7px rgba(140, 140, 140, 0.3)"
    })
    jQuery(e).find('.loupe_image img').css({
        "transition": ".2s",
        "opacity": "1"
    });
}

function mouseLeave(e) {
    jQuery(e).children().css({
        "z-index": "111",
        "transition": ".5s"
    })
    jQuery(e).find('.drop_container').css({
        "visibility": "hidden",
        "z-index": "0",
        "width": "237px",
        "border": "1px solid #ffffff"
    })
    jQuery(e).find('.loupe_image img').css({
        "transition": ".2s",
        "opacity": "0.3"
    });
}