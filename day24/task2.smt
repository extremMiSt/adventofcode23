(declare-const sx Int)
(declare-const sy Int)
(declare-const sz Int)

(declare-const svx Int)
(declare-const svy Int)
(declare-const svz Int)

(declare-const t1 Int)
(declare-const t2 Int)
(declare-const t3 Int)

(assert (= (+ sx (* t1 svx)) (+ 327367788702047 (* t1 20))))
(assert (= (+ sy (* t1 svy)) (+ 294752664325632 (* t1 51))))
(assert (= (+ sz (* t1 svz)) (+ 162080199489440 (* t1 36))))

(assert (= (+ sx (* t2 svx)) (+ 349323332347395 (* t2 -96))))
(assert (= (+ sy (* t2 svy)) (+ 429135322811787 (* t2 -480))))
(assert (= (+ sz (* t2 svz)) (+ 397812423558610 (* t2 -782))))

(assert (= (+ sx (* t3 svx)) (+ 342928768632768 (* t3 -69))))
(assert (= (+ sy (* t3 svy)) (+ 275572250031810 (* t3 104))))
(assert (= (+ sz (* t3 svz)) (+ 310926883862869 (* t3 -510))))

(check-sat)
(get-model)
