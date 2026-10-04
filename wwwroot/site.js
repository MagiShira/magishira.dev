// phone menu, CSS only shows it below 44rem when JS is present, without JS the links wrap under the brand
const menuBtn = document.querySelector('.menubtn');
if (menuBtn) {
    const set = open => { menuBtn.setAttribute('aria-expanded', open); document.querySelector('header').classList.toggle('open', open); };
    menuBtn.addEventListener('click', () => set(menuBtn.getAttribute('aria-expanded') !== 'true'));
    for (const a of document.querySelectorAll('#menu a')) a.addEventListener('click', () => set(false));
    addEventListener('keydown', e => { if (e.key === 'Escape') set(false); });
}

// light/dark toggle, the inline script in App.razor applies the stored choice before first paint
document.querySelector('.theme')?.addEventListener('click', () => {
    const cur = document.documentElement.dataset.theme || (matchMedia('(prefers-color-scheme: dark)').matches ? 'dark' : 'light');
    const next = cur === 'dark' ? 'light' : 'dark';
    const apply = () => { document.documentElement.dataset.theme = next; };
    document.startViewTransition ? document.startViewTransition(apply) : apply(); // crossfade where supported
    try { localStorage.setItem('theme', next); } catch { }
});

// photo strip, a real scroll container (so swiping works) that drifts on its own over 4 rendered copies and pauses
// on hover, touch or the pause button, reduced motion gets no drift and no copies but the buttons still work
const strip = document.querySelector('.marquee');
if (strip) {
    const track = strip.querySelector('.carousel');
    const reduced = matchMedia('(prefers-reduced-motion: reduce)').matches;
    const copies = reduced ? 1 : 4;
    const pauseBtn = document.querySelector('[data-strip="pause"]');
    let hover = false, held = false, holdUntil = 0, last = performance.now();
    const step = () => (track.querySelector('li').offsetWidth + 16); // one item plus the gap
    const loop = () => { if (copies > 1) { const w = track.scrollWidth / copies; if (strip.scrollLeft >= w) strip.scrollLeft -= w; else if (strip.scrollLeft < 0) strip.scrollLeft += w; } };
    const tick = now => {
        const dt = Math.min(now - last, 50); last = now;
        if (!reduced && !hover && !held && now > holdUntil && pauseBtn.getAttribute('aria-pressed') !== 'true') strip.scrollLeft += dt * 0.03; // ~30px/s
        loop();
        requestAnimationFrame(tick);
    };
    requestAnimationFrame(tick);
    strip.addEventListener('pointerenter', e => { if (e.pointerType === 'mouse') hover = true; });
    strip.addEventListener('pointerleave', () => hover = false);
    strip.addEventListener('touchstart', () => held = true, { passive: true });
    strip.addEventListener('touchend', () => { held = false; holdUntil = performance.now() + 1500; });
    strip.addEventListener('focusin', () => hover = true);
    strip.addEventListener('focusout', () => hover = false);
    for (const b of document.querySelectorAll('[data-strip]')) b.addEventListener('click', () => {
        const k = b.dataset.strip;
        if (k === 'pause') { b.setAttribute('aria-pressed', b.getAttribute('aria-pressed') !== 'true'); return; }
        if (k === 'prev' && strip.scrollLeft < step()) loop(), strip.scrollLeft += copies > 1 ? track.scrollWidth / copies : 0;
        holdUntil = performance.now() + 900; // let the smooth scroll finish before drifting again
        strip.scrollBy({ left: (k === 'next' ? 1 : -1) * step(), behavior: 'smooth' });
    });
}

// search and tag filter (Finder.razor) on /photos and /blog, two icons beside the title, search grows into a field and filter opens a popover of chips
// hidden without JS so the page just shows everything, state lives in ?q= and ?tag=
const finder = document.querySelector('.finder');
if (finder) {
    finder.hidden = false;
    const box = finder.querySelector('.searchbox'), q = box.querySelector('input');
    const sBtn = finder.querySelector('[data-act="search"]'), fBtn = finder.querySelector('[data-act="filter"]'), dot = fBtn.querySelector('.dot');
    const tray = document.getElementById(fBtn.getAttribute('popovertarget')), chips = [...tray.querySelectorAll('.chip')];
    const figs = [...document.querySelectorAll(finder.dataset.items)], empty = document.querySelector('.empty');
    const params = new URLSearchParams(location.search);
    let tag = params.get('tag') || '';
    q.value = params.get('q') || '';
    const openSearch = open => { box.classList.toggle('open', open); sBtn.setAttribute('aria-expanded', open); q.tabIndex = open ? 0 : -1; if (tray.matches(':popover-open')) setTimeout(place, 260); };
    const place = () => { // keep the popover under the filter icon, right-aligned, inside the viewport
        const r = fBtn.getBoundingClientRect(), w = tray.offsetWidth;
        const left = Math.max(16, Math.min(r.right - w, innerWidth - w - 16));
        tray.style.left = left + 'px';
        tray.style.top = r.bottom + 10 + 'px';
        tray.style.setProperty('--arrow-x', (r.left + r.width / 2 - left) + 'px'); // arrow points at the icon's centre
    };
    tray.addEventListener('beforetoggle', e => { if (e.newState === 'open') requestAnimationFrame(place); });
    tray.addEventListener('toggle', e => { fBtn.classList.toggle('on', e.newState === 'open'); if (e.newState === 'open') place(); });
    addEventListener('resize', () => tray.matches(':popover-open') && place());
    addEventListener('scroll', () => tray.matches(':popover-open') && place(), { passive: true });
    const apply = () => {
        const words = q.value.toLowerCase().split(/\s+/).filter(Boolean);
        let shown = 0;
        for (const f of figs) {
            const ok = (!tag || f.dataset.tags.split(' ').includes(tag)) && words.every(w => f.dataset.text.includes(w));
            f.hidden = !ok; shown += ok;
        }
        empty.hidden = shown > 0;
        dot.hidden = !tag;
        tray.querySelector('.clearf').hidden = !tag && !q.value;
        for (const c of chips) c.setAttribute('aria-pressed', c.dataset.tag === tag);
        const u = new URL(location); u.search = '';
        if (q.value) u.searchParams.set('q', q.value);
        if (tag) u.searchParams.set('tag', tag);
        history.replaceState(null, '', u);
    };
    sBtn.addEventListener('click', () => {
        if (!box.classList.contains('open')) { openSearch(true); q.focus(); }
        else if (!q.value) openSearch(false);
        else q.focus();
    });
    q.addEventListener('input', apply);
    q.addEventListener('keydown', e => { if (e.key === 'Escape') { q.value = ''; apply(); openSearch(false); sBtn.focus(); } });
    q.addEventListener('blur', () => setTimeout(() => { if (!q.value && document.activeElement !== q) openSearch(false); }, 200)); // let a tap on a neighbouring icon land before the layout shifts
    for (const c of chips) c.addEventListener('click', () => { tag = c.dataset.tag; apply(); });
    for (const c of document.querySelectorAll('[data-act="clear"]')) c.addEventListener('click', () => { tag = ''; q.value = ''; apply(); openSearch(false); });
    openSearch(!!q.value);
    apply();
}

// lightbox for the home photo strip and the /photos gallery, without JS the links still go somewhere useful
const box = document.querySelector('.lightbox');
if (box) {
    document.querySelector('.carousel, .gallery').addEventListener('click', e => {
        const a = e.target.closest('a[data-title]'); if (!a) return;
        e.preventDefault();
        const img = box.querySelector('img'), thumb = a.querySelector('img'); img.src = thumb.src; img.alt = thumb.alt;
        box.querySelector('strong').textContent = a.dataset.title;
        box.querySelector('strong').hidden = !a.dataset.title;
        box.querySelector('span').textContent = a.dataset.caption;
        box.showModal();
    });
    box.querySelector('.close').addEventListener('click', () => box.close());
    box.addEventListener('click', e => { if (e.target === box) box.close(); }); // backdrop
}

// tilt the CRT toward the pointer, without this (or on touch or reduced motion) the CSS sway animation runs instead
const rig = document.querySelector('.crt .rig');
if (rig) {
    const hero = rig.closest('.hero');
    if (matchMedia('(pointer:fine)').matches && !matchMedia('(prefers-reduced-motion: reduce)').matches) {
        hero.addEventListener('pointermove', e => {
            const r = hero.getBoundingClientRect();
            const x = (e.clientX - r.left) / r.width - .5, y = (e.clientY - r.top) / r.height - .5;
            rig.classList.add('live');
            rig.style.setProperty('--ry', `${-24 + x * 40}deg`);
            rig.style.setProperty('--rx', `${8 - y * 16}deg`);
        });
    }
    // tap or click to smack it
    rig.parentElement.addEventListener('click', () => { // click rather than pointerdown, a scroll that starts on the CRT shouldn't smack it
        rig.classList.remove('smack');
        void rig.offsetWidth; // restart the animation on rapid taps
        rig.classList.add('smack');
        const was = rig.classList.contains('blahaj');
        rig.classList.toggle('blahaj', Math.random() < 1 / 6); // the old /blahaj easter egg, 1 in 6 smacks
        if (was !== rig.classList.contains('blahaj')) { rig.classList.remove('osd-on'); void rig.offsetWidth; rig.classList.add('osd-on'); }
    });
    rig.addEventListener('animationend', e => { if (e.animationName === 'glitch') rig.classList.remove('smack'); if (e.animationName === 'osd') rig.classList.remove('osd-on'); });
}
