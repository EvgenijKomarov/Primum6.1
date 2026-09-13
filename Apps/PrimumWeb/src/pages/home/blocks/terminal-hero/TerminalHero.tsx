import { useEffect, useRef, useState } from "react";

/**
 * TerminalHero — имитация работы консоли с "бегущим" текстом.
 * Стиль подобран под пиксельную тему страницы (тёмный фон + кислотно-зелёный акцент).
 *
 * Использование:
 *   <TerminalHero />
 * Можно передать свои строки кода через проп lines, если нужно.
 */

type TerminalHeroProps = {
  lines?: string[];
  typingSpeedMs?: number;
  lineDelayMs?: number;
  restartDelayMs?: number;
};

const DEFAULT_LINES = [
  "$ npm create primumcode@latest",
  "> Куда движемся сегодня?",
  "$ cd primumcode/core",
  "$ git checkout -b feature/new-idea",
  "$ pip install -r requirements.txt",
  "> Устанавливаю зависимости... готово ✓",
  "$ python train_model.py --epochs=50",
  "> Epoch 12/50 | loss: 0.0842 | acc: 0.97",
  "$ docker build -t primumcode/app .",
  "> Собираю образ... [########--] 82%",
  "$ docker run -p 3000:3000 primumcode/app",
  "> Сервер запущен на порту 3000",
  "$ git add . && git commit -m \"идея стала кодом\"",
  "$ git push origin main",
  "> Ученик подключился к сессии",
  "$ npm run test",
  "> 34 passed, 0 failed (1.2s)",
  "$ ./run.sh --mode=learn",
  "> Компиляция прошла успешно ✓",
  "$ curl -s api.primumcode.dev/status",
  "> { \"status\": \"ok\", \"uptime\": \"99.98%\" }",
  "$ npm run build",
  "> Билд завершён за 4.3s",
  "$ ssh student@primumcode.dev",
  "> Добро пожаловать в PrimumCode",
  "$ echo \"там, где идеи становятся кодом\"",
];

const ACCENT = "#c6ff1a";

export default function TerminalHero({
  lines = DEFAULT_LINES,
  typingSpeedMs = 12,
  lineDelayMs = 180,
  restartDelayMs = 1000,
}: TerminalHeroProps) {
  const [history, setHistory] = useState<string[]>([]);
  const [current, setCurrent] = useState("");
  const [lineIndex, setLineIndex] = useState(0);
  const [charIndex, setCharIndex] = useState(0);
  const [cursorOn, setCursorOn] = useState(true);
  const scrollRef = useRef<HTMLDivElement>(null);

  // блинк курсора
  useEffect(() => {
    const t = setInterval(() => setCursorOn((v) => !v), 500);
    return () => clearInterval(t);
  }, []);

  // печать текста
  useEffect(() => {
    if (lineIndex >= lines.length) {
      const t = setTimeout(() => {
        setHistory([]);
        setCurrent("");
        setLineIndex(0);
        setCharIndex(0);
      }, restartDelayMs);
      return () => clearTimeout(t);
    }

    const fullLine = lines[lineIndex];

    if (charIndex < fullLine.length) {
      const t = setTimeout(() => {
        setCurrent(fullLine.slice(0, charIndex + 1));
        setCharIndex((c) => c + 1);
      }, typingSpeedMs);
      return () => clearTimeout(t);
    }

    const t = setTimeout(() => {
      setHistory((h) => [...h, fullLine]);
      setCurrent("");
      setCharIndex(0);
      setLineIndex((i) => i + 1);
    }, lineDelayMs);
    return () => clearTimeout(t);
  }, [charIndex, lineIndex, lines, typingSpeedMs, lineDelayMs, restartDelayMs]);

  // автопрокрутка вниз
  useEffect(() => {
    if (scrollRef.current) {
      scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
    }
  }, [history, current]);

  return (
    <div
      ref={scrollRef}
      style={{
        width: "100%",
        height: "30rem",
        background: "transparent",
        overflow: "hidden",
        fontFamily: "'JetBrains Mono', 'Courier New', monospace",
        fontSize: 13,
        lineHeight: 1.35,
        userSelect: "none",
      }}
    >
      {history.map((line, i) => (
        <div key={i} style={{ color: lineColor(line) }}>
          {line}
        </div>
      ))}
      <div style={{ color: lineColor(current || lines[lineIndex] || "") }}>
        {current}
        <span
          style={{
            display: "inline-block",
            width: 7,
            height: 13,
            marginLeft: 2,
            background: ACCENT,
            verticalAlign: "text-bottom",
            opacity: cursorOn ? 1 : 0,
          }}
        />
      </div>
    </div>
  );
}

function lineColor(line: string) {
  if (line.startsWith("$")) return "#e6e6e6";
  if (line.startsWith(">")) return ACCENT;
  return "#e6e6e6";
}