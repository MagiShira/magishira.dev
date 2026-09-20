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
    document.documentElement.dataset.theme = next;
    try { localStorage.setItem('theme', next); } catch { }
});

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
