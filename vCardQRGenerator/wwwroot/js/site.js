// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.



var vcard = {
    str_start: 'BEGIN:VCARD\nVERSION:3.0\n',
    str_vcard: 'BEGIN:VCARD\nVERSION:3.0\n',
    str_end: '\nEND:VCARD',
    goog_chart: 'http://chart.googleapis.com/chart?cht=qr&chs=200x200&chl=',
    form: [],
    set_field: function (data) {
        vcard.form = $('form').serializeArray();
        for (var i in vcard.form) {
            $("[name='" + vcard.form[i].name + "']").val(data[vcard.form[i].name])
        }
    },
    get_field: function (field) {
        for (var i in vcard.form) {
            if (vcard.form[i].name === field) {
                return vcard.form[i].value.replace(/^\s+|\s+$/g, "");
            }
        }
    },
    add_you: function () {
        var firstname = vcard.get_field("firstname"),
            lastname = vcard.get_field("lastname"),
            birthday = vcard.get_field('birthday'),
            gender = vcard.get_field('gender');

        vcard.str_vcard += 'N:' + lastname + ';' + firstname + '\n' +
            'FN:' + firstname + ' ' + lastname;
        // TODO convert date to american format
        if (birthday !== '') { vcard.str_vcard += '\nBDAY:' + birthday; }

        if (gender !== '') { vcard.str_vcard += '\nX-GENDER:' + gender; }
    },
    add_address: function () {
        var homestreet = vcard.get_field("homestreet"),
            homecity = vcard.get_field("homecity"),
            homeregion = vcard.get_field("homeregion"),
            homepost = vcard.get_field("homepost"),
            homecountry = vcard.get_field("homecountry"),
            orgstreet = vcard.get_field("orgstreet"),
            orgcity = vcard.get_field("orgcity"),
            orgregion = vcard.get_field("orgregion"),
            orgpost = vcard.get_field("orgpost"),
            orgcountry = vcard.get_field("orgcountry");

        if (homestreet + homecity + homeregion + homepost + homecountry !== '') {
            vcard.str_vcard += '\nADR;TYPE=home:;;' + homestreet + ';' + homecity + ';' + homeregion +
                ';' + homepost + ';' + homecountry;
        }
        if (orgstreet + orgcity + orgregion + orgpost + orgcountry !== '') {
            vcard.str_vcard += '\nADR;TYPE=work:;;' + orgstreet + ';' + orgcity + ';' + orgregion +
                ';' + orgpost + ';' + orgcountry;
        }
    },
    add_tel: function () {
        var personal = vcard.get_field("personaltel"),
            home = vcard.get_field("hometel"),
            work = vcard.get_field("orgtel");

        if (personal !== '') { vcard.str_vcard += '\nTEL;TYPE=mobile:' + personal; }

        if (home !== '') { vcard.str_vcard += '\nTEL;TYPE=home:' + home; }

        if (work !== '') { vcard.str_vcard += '\nTEL;TYPE=work:' + work; }
    },
    add_email: function () {
        var home = vcard.get_field("homeemail"),
            work = vcard.get_field("orgemail");

        if (home !== '') { vcard.str_vcard += '\nEMAIL;TYPE=internet,home:' + home; }

        if (work !== '') { vcard.str_vcard += '\nEMAIL;TYPE=internet,work:' + work; }
    },
    add_url: function () {
        var home = vcard.get_field("homeurl"),
            work = vcard.get_field("orgurl");

        if (home !== '') { vcard.str_vcard += '\nURL;TYPE=home:' + home; }

        if (work !== '') { vcard.str_vcard += '\nURL;TYPE=work:' + work; }
    },
    add_work: function () {
        var name = vcard.get_field("orgname"),
            title = vcard.get_field("orgtitle");

        if (name !== '') { vcard.str_vcard += '\nORG:' + name; }

        if (title !== '') { vcard.str_vcard += '\nTITLE:' + title; }
    },
    add_social: function () {
        var facebook = vcard.get_field("facebook"),
            twitter = vcard.get_field("twitter"),
            youtube = vcard.get_field("youtube"),
            skype = vcard.get_field("skype"),
            linkedin = vcard.get_field("linkedin"),
            flickr = vcard.get_field("flickr");

        if (facebook !== '') { vcard.str_vcard += '\nX-SOCIALPROFILE;type=facebook:' + facebook; }

        if (twitter !== '') { vcard.str_vcard += '\nX-SOCIALPROFILE;type=twitter:' + twitter; }

        if (linkedin !== '') { vcard.str_vcard += '\nX-SOCIALPROFILE;type=linkedin:' + linkedin; }

        if (flickr !== '') { vcard.str_vcard += '\nalbum;type=photo:' + flickr; }

        if (youtube !== '') { vcard.str_vcard += '\nalbum;type=video:' + youtube }

        if (skype !== '') { vcard.str_vcard += '\nX-SKYPE:' + skype; }
    },
    required_check: function () {
        var firstname = vcard.get_field("firstname"),
            lastname = vcard.get_field("lastname"),
            msg = 'Field%FIELD% %NAME% %VERB% required.',
            fields = [];

        if (firstname === '') { fields.push('First name'); }

        if (lastname === '') { fields.push('Last name'); }

        if (fields.length === 0) { return ''; }

        msg = msg.replace('%NAME%', fields.join(', '));

        msg = msg.replace('%FIELD%', (fields.length === 1) ? '' : 's');

        msg = msg.replace('%VERB%', (fields.length === 1) ? 'is' : 'are');

        return msg;
    },
    save: function () {
        vcard.form = $('form').serializeArray();

        var required_check_output = vcard.required_check();

        if (required_check_output !== '') {
            alert(required_check_output);
            return;
        }

        vcard.add_you();

        vcard.add_address();

        vcard.add_tel();

        vcard.add_email();

        vcard.add_url();

        vcard.add_work();

        vcard.add_social();

        vcard.str_vcard += vcard.str_end;

        $('textarea[name="vcard"]').val(vcard.str_vcard);

        var str_vcard_replaced = vcard.str_vcard.replace(/\n/g, '%0A');

        $('#qr').attr('src', vcard.goog_chart + str_vcard_replaced);

        vcard.str_vcard = vcard.str_start;

        fillVCardInfo();
        setQR(str_vcard_replaced);
    }
};



