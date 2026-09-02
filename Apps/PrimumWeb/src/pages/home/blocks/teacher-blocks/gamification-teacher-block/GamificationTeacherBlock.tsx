import { Block } from "@/pages/home/common-elements/Block/Block";



export const GamificationTeacherBlock = () => {
  return (
    <Block 
        title='Геймификация обучения'
        imageUrl='https://gb.ru/blog/wp-content/uploads/2022/07/2-3.jpg'
        plainText='
            Каждое занятие можно оценить. Чем выше оценка, тем больше монет и опыта получит ученик. За монеты он сможет покупать промокоды на донат в играх, что стимулирует детей учиться хорошо.

        '
        isReversed={true}
    />
  );
}