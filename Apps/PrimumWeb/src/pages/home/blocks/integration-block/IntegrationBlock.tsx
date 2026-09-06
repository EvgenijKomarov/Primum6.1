import { Block } from '../../common-elements/Block/Block';

export const IntegrationBlock = () => {
  return (
    <Block 
        title='Учебный процесс нового уровня'
        imageUrl='https://i.pinimg.com/736x/94/ec/d6/94ecd65aaae9ab2b047d5dcfc38aca5e.jpg'
        plainText='
            Мы предоставляем максимально гибкие и удобные возможности для обучения детей.
            Доступ к площадке возможен в том числе из мессенджеров для отслеживания прогресса в любое время.
        '
        isReversed={true}
    />
  );
}