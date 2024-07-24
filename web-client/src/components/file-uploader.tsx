import { Button } from '@/components/ui/button'
import { formatBytes } from '@/lib/utils'
import { Cross2Icon } from '@radix-ui/react-icons'
import Image from 'next/image'
import { useEffect, useState } from 'react'

type Props = {
  file?: File
  onRemove: () => void
  isMain: boolean
  onMainImageChange: () => void
  fileName: string
}

export default function FileCard({ file, onRemove, isMain, onMainImageChange, fileName }: Props) {
  const [preview, setPreview] = useState<string | null>(null)

  useEffect(() => {
    if (typeof file === 'object') {
      const objectUrl = URL.createObjectURL(file)
      setPreview(objectUrl)
      return () => URL.revokeObjectURL(objectUrl)
    }
    setPreview(`http://localhost:7001/api/files/${fileName}`)
  }, [file, fileName])

  return (
    <div className='relative flex items-center space-x-4'>
      <input
        type='radio'
        checked={isMain}
        onChange={onMainImageChange}
        className='shrink-0'
        aria-label={`Select ${fileName} as main image`}
      />
      <div className='flex flex-1 space-x-4'>
        {preview ? (
          <Image
            src={preview}
            alt={fileName}
            width={48}
            height={48}
            loading='lazy'
            className='aspect-square shrink-0 rounded-md object-cover'
          />
        ) : null}
        <div className='flex w-full flex-col gap-2'>
          <div className='space-y-px'>
            <p className='line-clamp-1 text-sm font-medium text-foreground/80'>{fileName}</p>
            <p className='text-xs text-muted-foreground'>{formatBytes(file ? file.size : 0)}</p>
          </div>
        </div>
      </div>
      <div className='flex items-center gap-2'>
        <Button type='button' variant='outline' size='icon' className='size-7' onClick={onRemove}>
          <Cross2Icon className='size-4 ' aria-hidden='true' />
          <span className='sr-only'>Remove file</span>
        </Button>
      </div>
    </div>
  )
}
