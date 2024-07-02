import { CardContent } from '@/components/ui/card'
import { Carousel, CarouselContent, CarouselItem } from '@/components/ui/carousel'
import { Trash2 } from 'lucide-react'
import Image from 'next/image'

type Props = {
  imageURLs: string[]
  handleDelete: (index: number) => void
}

export default function ImageDisplay({ imageURLs, handleDelete }: Props) {
  return (
    <div className='flex items-center justify-center w-full h-full '>
      <Carousel className='w-full max-w-xs'>
        <CarouselContent>
          {imageURLs.map((url, index) => (
            <CarouselItem key={index}>
              <CardContent className='h-[150px] w-full relative flex items-center justify-center bg-black'>
                <div className='absolute inset-0 flex items-center justify-center'>
                  <Image src={url} alt={`image ${index}`} width={100} height={100} />
                </div>
                <button
                  type='button'
                  className='absolute right-[-10px] top-[-10px]'
                  onClick={() => handleDelete(index)}
                >
                  <Trash2
                    size={35}
                    className='flex items-center justify-center text-primary-backgroudPrimary bg-white rounded-md p-2 m-5'
                  />
                </button>
              </CardContent>
            </CarouselItem>
          ))}
        </CarouselContent>
      </Carousel>
    </div>
  )
}
